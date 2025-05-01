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

        public async Task<IdentityResult> SyncUserRolesAsync(string userId, IEnumerable<string> selectedRoles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found.");

            var currentRoles = await _userManager.GetRolesAsync(user);

            var rolesToAdd = selectedRoles.Except(currentRoles);
            var rolesToRemove = currentRoles.Except(selectedRoles);

            var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
            if (!addResult.Succeeded)
                return addResult;

            var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            return removeResult;
        }


        public async Task<IEnumerable<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found", nameof(userId));
            }

            return await _userManager.GetRolesAsync(user);
        }


        public IEnumerable<ApplicationUser> GetAllUsers()
        {

            var users = _userManager.Users;
            return users;


        }

        public async Task<IEnumerable<string>> GetUserIdsInRoleAsync(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            return users.Select(u => u.Id);
        }

        public async Task<IdentityResult> AddUserToRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });
            return await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });
            return await _userManager.RemoveFromRoleAsync(user, roleName);
        }
    }
}
