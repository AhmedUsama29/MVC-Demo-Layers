namespace Demo.Presentation.ViewModels.DepartmentViewModels
{
    public class DepartmentEditViewModel
    {

        public string Name { get; set; } = null!;

        public int Code { get; set; }

        public string? Description { get; set; }

        public DateTime? DateOfCreation { get; set; }

    }
}
