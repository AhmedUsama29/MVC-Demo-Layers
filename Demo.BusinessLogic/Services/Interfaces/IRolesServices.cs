using Demo.BusinessLogic.DTOs.RolesDtos;
using Microsoft.AspNetCore.Identity;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IRolesServices
    {

        public Task<IEnumerable<string?>> GetAllRolesAsync();
        public IEnumerable<GetRolesDto> GetAllRolesDetails(string? RoleSearchName);
        public Task<GetRolesDto?> GetRoleByIdAsync(string id);
        public Task<IdentityResult> CreateRoleAsync(CreateRolesDto createRolesDto);
        public Task<IdentityResult> UpdateRoleAsync(UpdateRoleDto updateRoleDto);
        public Task<IdentityResult> DeleteRoleAsync(string id);

    }
}