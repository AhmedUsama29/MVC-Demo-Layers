namespace Demo.Presentation.ViewModels.UserViewModels
{
    public class DeleteUserViewModel
    {

        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
