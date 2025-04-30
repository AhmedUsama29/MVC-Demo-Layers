using AutoMapper;
using Demo.BusinessLogic.DTOs.RolesDtos;
using Demo.BusinessLogic.DTOs.UserDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Classes
{
    public class UserServices(UserManager<ApplicationUser> _userManager,
                                IMapper _mapper) : IUserServices
    {

        public async Task<IEnumerable<GetUserDto>> GetAllUsersWithRoles(string? SearchValue)
        {
            var usersQuery = _userManager.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchValue))
                usersQuery = usersQuery.Where(U => U.Email.ToLower().Contains(SearchValue.ToLower()));

            var usersList = await usersQuery.Select(U => new GetUserDto
            {
                Id = U.Id,
                FirstName = U.FirstName,
                LastName = U.LastName,
                Email = U.Email,
                PhoneNumber = U.PhoneNumber,
            }).ToListAsync();

            foreach (var user in usersList)
            {
                var userEntity = await _userManager.FindByIdAsync(user.Id);
                user.Roles = await _userManager.GetRolesAsync(userEntity);
            }

            return usersList;
        }



        public async Task<GetUserDto?> GetUserByIdAsync(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return null;

            var userDto = _mapper.Map<GetUserDto>(user);

            var roles = await _userManager.GetRolesAsync(user);
            userDto.Roles = roles.ToList();

            return userDto;

        }

        public async Task<IdentityResult> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(updateUserDto.Id);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.PhoneNumber = updateUserDto.PhoneNumber;

            return await _userManager.UpdateAsync(user);
        }


        public async Task<IdentityResult> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            return await _userManager.DeleteAsync(user);
        }



    }
}
