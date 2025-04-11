using Demo.BusinessLogic.DTOs.EmployeeDtos;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModels;
using Demo.DataAccess.Models.SharedModels;
using Demo.Presentation.ViewModels.DepartmentViewModels;
using Demo.Presentation.ViewModels.EmployeeViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService, 
                                        ILogger<EmployeesController> _logger , IWebHostEnvironment _env) : Controller
    {
        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployees();
            return View(Employees);
        }

        //Don't Forget to Set the (soft) Deleted Employees to not show in the list [Get All] & ... 


        #region Create

        [HttpGet]

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto createEmployeeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int res = _employeeService.CreateEmployee(createEmployeeDto);
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
             return View(createEmployeeDto);
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
                var MappedEmp = new EmployeeEditViewModel() {

                    Name = emp.Name,
                    Age = emp.Age,
                    Address = emp.Address,
                    Salary = emp.Salary,
                    IsActive = emp.IsActive,
                    Email = emp.Email,
                    PhoneNumber = emp.PhoneNumber,
                    HiringDate = emp.HiringDate,
                    Gender = (Gender)Enum.Parse(typeof(Gender),emp.Gender),
                    EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), emp.EmployeeType)
                };
                return View(MappedEmp);
            }
        }

        [HttpPost]

        public IActionResult Edit([FromRoute]int id,EmployeeEditViewModel editViewModel) 
        {

            if (!ModelState.IsValid) return View(editViewModel);

            try
            {

                    var UpdateEmpDto = new UpdateEmployeeDto() {
                    
                        Id = id,
                        Name = editViewModel.Name,
                        Age = editViewModel.Age,
                        Address = editViewModel.Address,
                        Salary = editViewModel.Salary,
                        IsActive = editViewModel.IsActive,
                        Email = editViewModel.Email,
                        PhoneNumber = editViewModel.PhoneNumber,
                        HiringDate= editViewModel.HiringDate,
                        Gender=editViewModel.Gender,
                        EmployeeType=editViewModel.EmployeeType
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

    }
}
