using Demo.BusinessLogic.DTOs.RolesDtos;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IRolesServices
    {
        IEnumerable<GetRolesDto> GetAllRoles(string? RoleSearchName);
    }
}