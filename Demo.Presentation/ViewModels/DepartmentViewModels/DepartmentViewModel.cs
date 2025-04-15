using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.DepartmentViewModels
{
    public class DepartmentViewModel
    {

        [Required(ErrorMessage = "Name is Required")]

        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Code is Required")]

        public int Code { get; set; }

        public string? Description { get; set; }

        public DateTime? DateOfCreation { get; set; }

    }
}
