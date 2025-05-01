using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.UserViewModels
{
    public class UpdateUserViewModel
    {

        public string Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [MaxLength(11, ErrorMessage = "Phone number cannot exceed 11 characters")]
        public string PhoneNumber { get; set; } 

    }
}
