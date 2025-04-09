using Demo.BusinessLogic.DTOs;

namespace Demo.BusinessLogic.Services
{
    public interface IDepartmentService
    {
        int CreateDepartment(CreateDepartmentDto createDepartmentDto);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentByID(int id);
        int UpdateDepartment(UpdateDepartmentDto updateDepartmentDto);
    }
}