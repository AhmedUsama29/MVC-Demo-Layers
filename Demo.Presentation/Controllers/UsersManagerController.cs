using Demo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;

namespace Demo.Presentation.Controllers
{
    public class UsersManagerController(IUserServices _userServices,
        ILogger<DepartmentController> _logger,
        IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index(string? UserSearchEmail)
        {
            var users = await _userServices.GetAllUsersWithRoles(UserSearchEmail);

            return View(users);
        }

    }
}
