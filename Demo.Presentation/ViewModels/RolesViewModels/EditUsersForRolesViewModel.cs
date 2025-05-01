using Demo.BusinessLogic.DTOs.UserDtos;

namespace Demo.Presentation.ViewModels.RolesViewModels
{
    public class EditUsersForRolesViewModel
    {
        public string RoleId { get; set; } = null!;
        public List<string> RoleUsers { get; set; } = new();
        public string? RoleName { get; set; }
        public List<GetUserDto> AllUsers { get; set; } = new();
    }

}
