using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.BusinessLogic.DTOs.RolesDtos;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.Presentation.ViewModels.DepartmentViewModels;
using Demo.Presentation.ViewModels.RolesViewModels;
using Demo.Presentation.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    [Authorize]
    public class RolesController(IRolesServices _rolesServices,
        IUserServices _userServices,
        ILogger<DepartmentController> _logger,
        IWebHostEnvironment _env) : Controller
    {

        public IActionResult Index(string? RoleSearchName)
        {

            var model = _rolesServices.GetAllRolesDetails(RoleSearchName);

            return View(model);
        }

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var role = new CreateRolesDto
                    {
                        Name = model.Name
                    };

                    var result = await _rolesServices.CreateRoleAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        foreach (var error in result.Errors)
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

            return View(model);
        }
        #endregion

        #region Details


        public async Task<IActionResult> Details(string id)
        {
            var role = await _rolesServices.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {

            var role = await _rolesServices.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var model = new UpdateRoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromRoute]string? id, UpdateRoleViewModel viewModel)
        {

            if (string.IsNullOrWhiteSpace(id)) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {

                    var res = await _rolesServices.UpdateRoleAsync(new UpdateRoleDto
                    {
                        Id = id,
                        Name = viewModel.Name
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

        #region User Management

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUser(string id)
        {
            var role = await _rolesServices.GetRoleByIdAsync(id);
            if (role == null) return NotFound();

            var allUsers = await _userServices.GetAllUsersWithRoles(null);
            var roleUsers = (await _userServices.GetUserIdsInRoleAsync(role.Name))?.ToList() ?? new();

            var model = new EditUsersForRolesViewModel
            {
                RoleId = id,
                RoleName = role.Name,
                RoleUsers = roleUsers,
                AllUsers = allUsers.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string id, EditUsersForRolesViewModel model)
        {
            var role = await _rolesServices.GetRoleByIdAsync(id);
            if (role == null)
                return NotFound();

            var currentUserIds = await _userServices.GetUserIdsInRoleAsync(role.Name);

            var selectedUserIds = model.RoleUsers ?? new List<string>();

            var userIdsToAdd = selectedUserIds.Except(currentUserIds).ToList();

            var userIdsToRemove = currentUserIds.Except(selectedUserIds).ToList();

            foreach (var userId in userIdsToAdd)
            {
                var result = await _userServices.AddUserToRoleAsync(userId, role.Name);
            }

            foreach (var userId in userIdsToRemove)
            {
                var result = await _userServices.RemoveUserFromRoleAsync(userId, role.Name);
            }

            return RedirectToAction(nameof(Index));
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
            var role = await _rolesServices.GetRoleByIdAsync(id);
            if (role is null) return NotFound();

            return View(role);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm([FromRoute] string id)
        {

            if (string.IsNullOrEmpty(id)) return BadRequest();
            try
            {
                var res = await _rolesServices.DeleteRoleAsync(id);
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
