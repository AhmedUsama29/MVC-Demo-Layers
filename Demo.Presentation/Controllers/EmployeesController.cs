using Demo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService) : Controller
    {
        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployees();
            return View(Employees);
        }

        //Don't Forget to Set the (soft) Deleted Employees to not show in the list [Get All] & ... 


    }
}
