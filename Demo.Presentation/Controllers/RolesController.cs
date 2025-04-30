using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.Presentation.ViewModels.DepartmentViewModels;
using Demo.Presentation.ViewModels.RolesViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class RolesController(IRolesServices _rolesServices
        , RoleManager<IdentityRole> _roleManager,   //remove
        ILogger<DepartmentController> _logger,
        IWebHostEnvironment _env) : Controller
    {
        public IActionResult Index(string? RoleSearchName)
        {

            //var Roles = _roleManager.Roles;
            //IEnumerable<GetRolesViewModel> model = Roles.Select(r => new GetRolesViewModel
            //{
            //    id = r.Id,
            //    Name = r.Name
            //});

            var model = _rolesServices.GetAllRoles(RoleSearchName);

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var role = new IdentityRole
                    {
                        Name = model.Name
                    };
                    var result = _roleManager.CreateAsync(role).Result;
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


    }
}
