using AutoMapper;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using Demo.BusinessLogic.DTOs.RolesDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModels;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Classes
{
    public class RolesServices(RoleManager<IdentityRole> _roleManager,
                                IMapper _mapper) : IRolesServices
    {
        public async Task<IEnumerable<string?>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles
            .Select(r => r.Name)
            .ToListAsync();

            return roles;

        }
        public IEnumerable<GetRolesDto> GetAllRolesDetails(string? RoleSearchName)
        {

            IEnumerable<IdentityRole> Roles;

            if (string.IsNullOrEmpty(RoleSearchName))
            {
                Roles = _roleManager.Roles;
            }
            else
            {
                Roles = _roleManager.Roles.Where(e => (e.Name.ToLower()).Contains(RoleSearchName.ToLower()));
            }

            return _mapper.Map<IEnumerable<GetRolesDto>>(Roles);
        }

        public async Task<GetRolesDto?> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            return role is null ? null
                : _mapper.Map<GetRolesDto>(role);
        }

        public async Task<IdentityResult> CreateRoleAsync(CreateRolesDto createRolesDto)
        {

            var role = _mapper.Map<IdentityRole>(createRolesDto);

            var res = await _roleManager.CreateAsync(role);
            
            return res;
        }


        public async Task<IdentityResult> UpdateRoleAsync(UpdateRoleDto updateRoleDto)
        {

            var role = await _roleManager.FindByIdAsync(updateRoleDto.Id);

            if (role == null)
                return IdentityResult.Failed(new IdentityError { Description = "Role not found." });

            role.Name = updateRoleDto.Name;

            var result = await _roleManager.UpdateAsync(role);

            return result;

        }

        public async Task<IdentityResult> DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role is null)
                return IdentityResult.Failed(new IdentityError { Description = "Role not found." });

            return await _roleManager.DeleteAsync(role);
        }

       
    }
}
