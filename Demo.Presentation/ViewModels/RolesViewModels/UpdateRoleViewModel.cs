using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.RolesViewModels
{
    public class UpdateRoleViewModel
    {

        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "Role name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Role name must be between 2 and 50 characters.")]
        public string Name { get; set; }

    }
}
