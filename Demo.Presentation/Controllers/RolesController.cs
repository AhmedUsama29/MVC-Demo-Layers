using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.BusinessLogic.DTOs.RolesDtos;
using Demo.BusinessLogic.Services.Classes; // remove
using Demo.BusinessLogic.Services.Interfaces;
using Demo.Presentation.ViewModels.DepartmentViewModels;
using Demo.Presentation.ViewModels.RolesViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class RolesController(IRolesServices _rolesServices,
        ILogger<DepartmentController> _logger,
        IWebHostEnvironment _env) : Controller
    {
        public IActionResult Index(string? RoleSearchName)
        {

            var model = _rolesServices.GetAllRoles(RoleSearchName);

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

        

        #endregion
    }
}
