using AutoMapper;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModels;
using Demo.DataAccess.Repositories.Interfaces;


namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository, IMapper _mapper) : IEmployeeService
    {

        public IEnumerable<GetEmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {

            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(EmployeeSearchName)){
                employees = _employeeRepository.GetAll();
            }
            else
            {
                employees = _employeeRepository.GetAll(e => (e.Name.ToLower()).Contains(EmployeeSearchName.ToLower()));
            }

            //Dest => Source
            return _mapper.Map<IEnumerable<GetEmployeeDto>>(employees);
        }

        public EmployeeDetailsDto? GetEmployeeByID(int id)
        {
            var emp = _employeeRepository.GetById(id);

            return emp is null ? null
                : _mapper.Map<EmployeeDetailsDto>(emp);
        }

        public int CreateEmployee(CreateEmployeeDto createemployeeDto)
        {
            var emp = _mapper.Map<Employee>(createemployeeDto);

            return _employeeRepository.Add(emp);
        }


        public int UpdateEmployee(UpdateEmployeeDto updateemployeeDto)
        {
            var emp = _mapper.Map<Employee>(updateemployeeDto);

            return _employeeRepository.Update(emp);
        }

        public bool DeleteEmployee(int id)
        {
            var emp = _employeeRepository.GetById(id);
            if (emp is null) return false;
            else 
            { 
                emp.IsDeleted = true;
                int res = _employeeRepository.Update(emp);
                return (res > 0) ? true : false;
            }
        }


    }
}
