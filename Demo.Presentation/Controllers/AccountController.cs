using Demo.Presentation.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid) 
            {

            }
            return View(viewModel);
        }

    }
}
