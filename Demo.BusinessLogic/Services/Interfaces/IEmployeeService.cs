using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {

        int CreateEmployee(CreateEmployeeDto createemployeeDto);
        IEnumerable<GetEmployeeDto> GetAllEmployees(string ?EmployeeSearchName);
        EmployeeDetailsDto? GetEmployeeByID(int id);
        int UpdateEmployee(UpdateEmployeeDto updateemployeeDto);
        bool DeleteEmployee(int id);

    }
}
