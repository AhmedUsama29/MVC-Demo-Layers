using Demo.BusinessLogic.DTOs.RolesDtos;
using Demo.BusinessLogic.DTOs.UserDtos;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.Presentation.ViewModels.RolesViewModels;
using Demo.Presentation.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;

namespace Demo.Presentation.Controllers
{
    [Authorize]
    public class UsersManagerController(IUserServices _userServices,
        ILogger<DepartmentController> _logger,
        IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index(string? UserSearchEmail)
        {
            var users = await _userServices.GetAllUsersWithRoles(UserSearchEmail);

            return View(users);
        }

        #region Details

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userServices.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {

            var user = await _userServices.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UpdateUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Update([FromRoute] string? id, UpdateUserViewModel viewModel)
        {

            if (string.IsNullOrWhiteSpace(id)) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {

                    var res = await _userServices.UpdateUserAsync(new UpdateUserDto
                    {
                        Id = id,
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                        PhoneNumber = viewModel.PhoneNumber,
                    });

                    if (res.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        foreach (var error in res.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                    }
                }
                catch (Exception ex)
                {
                    if (_env.IsDevelopment())
                    {
                        ModelState.AddModelError(String.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            }
            return View(viewModel);
        }

        #endregion

        #region Roles Mangement

        [HttpGet]
        public IActionResult AddOrRemoveRoles(string id)
        {

            var model = new EditUserRolesViewModel()
            {
                UserId = id,
                UserRoles = _userServices.GetUserRolesAsync(id).Result.ToList()
            };

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> AddOrRemoveRolesAsync([FromRoute] string id, EditUserRolesViewModel model)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            try
            {
                var res = await _userServices.SyncUserRolesAsync(id, model.UserRoles);
                if (res.Succeeded) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(String.Empty, "Error in updating user roles");
                }
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
                else
                {
                    _logger.LogError(ex.Message);
                }
            }
            return View(model);
        }

        #endregion

        #region Delete


        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var user = await _userServices.GetUserByIdAsync(id);
            if (user is null) return NotFound();

            var model = new DeleteUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
            };

            return View(model);

        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm([FromRoute] string id)
        {

            if (string.IsNullOrEmpty(id)) return BadRequest();

            try
            {
                var res = await _userServices.DeleteUserAsync(id);
                if (res.Succeeded) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(String.Empty, "Error in deleting department");
                }
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
                else
                {
                    _logger.LogError(ex.Message);
                }
            }
            return RedirectToAction(nameof(Delete), new { id });
        }


        #endregion
    }
}
