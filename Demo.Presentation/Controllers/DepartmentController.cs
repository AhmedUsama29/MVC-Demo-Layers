using Demo.BusinessLogic.DTOs;
using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.Presentation.ViewModels.DepartmentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    [Authorize]
    public class DepartmentController(IDepartmentService _departmentService,
        ILogger<DepartmentController> _logger,
        IWebHostEnvironment _env) : Controller
    {
        public IActionResult Index()
        {

            var departments = _departmentService.GetAllDepartments();

            return View(departments);
        }

        #region Create Department
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var dto = new CreateDepartmentDto()
                    {
                        Name = departmentViewModel.Name,
                        Code = departmentViewModel.Code,
                        Description = departmentViewModel.Description,
                        DateOfCreation = departmentViewModel.DateOfCreation
                    };

                    int res = _departmentService.CreateDepartment(dto);
                    var message = string.Empty;

                    if (res > 0) message = "Department created successfully";
                    else
                    {
                                //ModelState.AddModelError(String.Empty, "Error in creating department");
                                message = "Error in creating department";
                    }

                    TempData["Message"] = message;
                    return RedirectToAction(nameof(Index));
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
                return View(departmentViewModel);
        }
        #endregion

        #region Department Details

        public IActionResult Details(int? id) 
        {

            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = _departmentService.GetDepartmentByID(id.Value);
            if (department is null) return NotFound();

            return View(department);

        }

        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = _departmentService.GetDepartmentByID(id.Value);
            if (department is null) return NotFound();
            else
            {
                var departmentViewModel = new DepartmentViewModel()
                {
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description,
                    DateOfCreation = department.DateOfCreation
                };

                return View(departmentViewModel);
            }

        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id,DepartmentViewModel departmentEditViewModel) 
        {

        
            if (!ModelState.IsValid) return View(departmentEditViewModel);

            try 
            {

                var UpdatedDept = new UpdateDepartmentDto() {
                    Id = id,
                    Code = departmentEditViewModel.Code,
                    Description = departmentEditViewModel.Description,
                    Name = departmentEditViewModel.Name,
                    DateOfCreation = departmentEditViewModel.DateOfCreation
                };
                var res = _departmentService.UpdateDepartment(UpdatedDept);

                if (res > 0) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(String.Empty, "Error in editing department");
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
            return View(departmentEditViewModel);
        }

        #endregion

        #region Delete

        [HttpGet]

        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = _departmentService.GetDepartmentByID(id.Value);
            if (department is null) return NotFound();
            return View(department);

        }

        [HttpPost]

        public IActionResult Delete(int id)
        { 
        
            if (id == 0) return BadRequest();
            try
            {
                var res = _departmentService.DeleteDepartment(id);
                if (res) return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Delete),new { id });
        }

        #endregion
    }
}
