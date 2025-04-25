using AutoMapper;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using Demo.BusinessLogic.Services.AttatchmentService;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModels;
using Demo.DataAccess.Repositories.Interfaces;


namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IUnitOfWork _unitOfWork,
                                 IMapper _mapper,
                                 IAttatchmentService _attatchmentService) : IEmployeeService
    {
        private readonly IAttatchmentService _AttatchmentService = _attatchmentService;

        public IEnumerable<GetEmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {

            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(EmployeeSearchName)){
                employees = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.GetAll(e => (e.Name.ToLower()).Contains(EmployeeSearchName.ToLower()));
            }

            //Dest => Source
            return _mapper.Map<IEnumerable<GetEmployeeDto>>(employees);
        }

        public EmployeeDetailsDto? GetEmployeeByID(int id)
        {
            var emp = _unitOfWork.EmployeeRepository.GetById(id);

            return emp is null ? null
                : _mapper.Map<EmployeeDetailsDto>(emp);
        }

        public int CreateEmployee(CreateEmployeeDto createemployeeDto)
        {

            var emp = _mapper.Map<Employee>(createemployeeDto);

            var imageName = _AttatchmentService.Upload(createemployeeDto.Image, "Images");

            emp.ImageName = imageName;

            _unitOfWork.EmployeeRepository.Add(emp);
            return _unitOfWork.SaveChanges();
        }


        public int UpdateEmployee(UpdateEmployeeDto updateemployeeDto)
        {

            //var existingEmp = _unitOfWork.EmployeeRepository.GetById(updateemployeeDto.Id);

            //if (existingEmp is null) return 0;

            var emp = _mapper.Map<Employee>(updateemployeeDto);

            if (updateemployeeDto.Image != null)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images");
                var filePath = Path.Combine(folderPath,updateemployeeDto.Image.Name);
                _AttatchmentService.Delete(filePath);
                var imageName = _AttatchmentService.Upload(updateemployeeDto.Image, "Images");
                emp.ImageName = imageName;
            }
            else
            {
                emp.ImageName = updateemployeeDto.ImageName;
            }

            _unitOfWork.EmployeeRepository.Update(emp);
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var emp = _unitOfWork.EmployeeRepository.GetById(id);
            if (emp is null) return false;
            else 
            { 
                emp.IsDeleted = true;
                _unitOfWork.EmployeeRepository.Update(emp);
                return (_unitOfWork.SaveChanges() > 0) ? true : false;
            }
        }


    }
}
