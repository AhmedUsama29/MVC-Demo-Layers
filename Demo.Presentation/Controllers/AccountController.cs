using Demo.DataAccess.Models.IdentityModels;
using Demo.Presentation.Helper;
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

        #region LogOut
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        } 
        #endregion


        #region ForgetPassword

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]

        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel) 
        {

            if (ModelState.IsValid) 
            {
                var user = _userManager.FindByEmailAsync(viewModel.Email).Result;

                if (user is not null)
                {

                    var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;

                    var url = Url.Action("ResetPassword", "Account", new { email = viewModel.Email , token }, Request.Scheme);

                    var email = new Email()

                    {

                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = url

                    };

                    bool isMailSent = EmailSettings.SendEmail(email);
                    if (isMailSent) return RedirectToAction(nameof(CheckYourInbox));

                }
                else 
                { 
                ModelState.AddModelError(string.Empty, "Somthing Went Wrong, Please Try Again");
                }
            }
            return View(nameof(ForgetPassword));
        }

        public IActionResult CheckYourInbox()
        {

            return View();
        }

        #endregion



        #region Reset Password

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {

            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]

        public IActionResult ResetPassword(ResetPasswordViewModel viewModel) 
        {

            if (ModelState.IsValid) 
            {

                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

                if (email is null || token is null) return BadRequest();
                else 
                { 
                
                    var user = _userManager.FindByEmailAsync(email).Result;
                    if (user is not null)
                    {

                        var res = _userManager.ResetPasswordAsync(user, token, viewModel.Password).Result;

                        if (res.Succeeded) return RedirectToAction(nameof(LogIn));
                    }
                    else 
                    { 
                    ModelState.AddModelError(string.Empty, "Error");
                    }

                }

            }
            return View(viewModel);

        }

        #endregion

    }
}
