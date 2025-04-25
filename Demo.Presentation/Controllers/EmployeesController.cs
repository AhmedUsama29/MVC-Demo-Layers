using Demo.BusinessLogic.DTOs.EmployeeDtos;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModels;
using Demo.DataAccess.Models.SharedModels;
using Demo.Presentation.ViewModels.DepartmentViewModels;
using Demo.Presentation.ViewModels.EmployeeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    [Authorize]
    public class EmployeesController(IEmployeeService _employeeService, 
                                        ILogger<EmployeesController> _logger , 
                                        IWebHostEnvironment _env) : Controller
    {
        public IActionResult Index(string? EmployeeSearchName)
        {


            var Employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return View(Employees);
        }


        #region Create

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var CreateEmployeeDto = new CreateEmployeeDto() 
                    {
                        Name= employeeViewModel.Name,
                        Age = employeeViewModel.Age,
                        Address = employeeViewModel.Address,
                        Salary = employeeViewModel.Salary,
                        IsActive = employeeViewModel.IsActive,
                        Email = employeeViewModel.Email,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        HiringDate = employeeViewModel.HiringDate,
                        DepartmentId = employeeViewModel.DepartmentId,
                        EmployeeType = employeeViewModel.EmployeeType,
                        Gender = employeeViewModel.Gender,
                        Image = employeeViewModel.Image
                    };

                    int res = _employeeService.CreateEmployee(CreateEmployeeDto);
                    if (res > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Error in creating employee");
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
             return View(employeeViewModel);
        }

        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeByID(id.Value);
            if (employee == null) return NotFound();
            return View(employee);
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
            var emp = _employeeService.GetEmployeeByID(id.Value);
            if (emp is null) return NotFound();
            else
            {
                var MappedEmp = new EmployeeViewModel() {

                    Name = emp.Name,
                    Age = emp.Age,
                    Address = emp.Address,
                    Salary = emp.Salary,
                    IsActive = emp.IsActive,
                    Email = emp.Email,
                    PhoneNumber = emp.PhoneNumber,
                    HiringDate = emp.HiringDate,
                    Gender = (Gender)Enum.Parse(typeof(Gender),emp.Gender),
                    EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), emp.EmployeeType),
                    DepartmentId = emp.DepartmentId,
                    ImageName = emp.ImageName
                };

                TempData["OldImageName"] = emp.ImageName;

                return View(MappedEmp);
            }
        }

        [HttpPost]

        public IActionResult Edit([FromRoute]int? id,EmployeeViewModel editViewModel) 
        {

            if (!id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return View(editViewModel);

            try
            {

                    var UpdateEmpDto = new UpdateEmployeeDto() {
                    
                        Id = id.Value,
                        Name = editViewModel.Name,
                        Age = editViewModel.Age,
                        Address = editViewModel.Address,
                        Salary = editViewModel.Salary,
                        IsActive = editViewModel.IsActive,
                        Email = editViewModel.Email,
                        PhoneNumber = editViewModel.PhoneNumber,
                        HiringDate= editViewModel.HiringDate,
                        Gender=editViewModel.Gender,
                        EmployeeType=editViewModel.EmployeeType,
                        DepartmentId = editViewModel.DepartmentId,
                        Image = editViewModel.Image,
                        ImageName = TempData["OldImageName"] as string

                    };
                    int res = _employeeService.UpdateEmployee(UpdateEmpDto);

                    if (res > 0) 
                    {
                        return RedirectToAction(nameof(Index)); 
                    }
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

            return View(editViewModel);

        }

        #endregion

        #region Delete

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeByID(id.Value);
            if (employee == null) return NotFound();
            return View(employee);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                bool res = _employeeService.DeleteEmployee(id);
                if (res)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Error in deleting employee");
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

            return RedirectToAction(nameof(Delete), new {id});
        }

        #endregion

    }
}
