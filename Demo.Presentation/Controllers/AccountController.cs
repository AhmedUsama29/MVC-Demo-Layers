using Demo.DataAccess.Models.IdentityModels;
using Demo.Presentation.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    
    public class AccountController(UserManager<ApplicationUser> _userManager,
                                   SignInManager<ApplicationUser> _signInManager) : Controller
    {
        #region Register
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

                var user = new ApplicationUser
                {
                    UserName = viewModel.UserName,
                    Email = viewModel.Email,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName
                };

                var result = _userManager.CreateAsync(user, viewModel.Password).Result;
                if (result.Succeeded) return RedirectToAction("LogIn");
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(viewModel);
        }
        #endregion

        #region LogIn

        [HttpGet]

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]

        public IActionResult LogIn(LoginViewModel viewModel) 
        {

            if (ModelState.IsValid) 
            {

                var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (user is null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }
                else 
                { 
                var flag = _userManager.CheckPasswordAsync(user, viewModel.Password).Result;

                    if (flag) 
                    {
                        var res = _signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, false).Result;
                        if(res.Succeeded) return RedirectToAction("Index", "Home"); 
                    }
                    else ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }

            }

            return View(viewModel);
        }

        #endregion

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }

    }
}
