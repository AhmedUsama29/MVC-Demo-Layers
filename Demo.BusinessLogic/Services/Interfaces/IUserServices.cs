using Demo.BusinessLogic.DTOs.RolesDtos;
using Demo.BusinessLogic.DTOs.UserDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IUserServices
    {

        Task<IEnumerable<GetUserDto>> GetAllUsersWithRoles(string? UserSearchName);
        public Task<GetUserDto?> GetUserByIdAsync(string id);
        public Task<IdentityResult> UpdateUserAsync(UpdateUserDto updateUserDto);
        public Task<IdentityResult> DeleteUserAsync(string id);

    }
}
