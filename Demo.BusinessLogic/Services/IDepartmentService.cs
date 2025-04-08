using Demo.BusinessLogic.DTOs;

namespace Demo.BusinessLogic.Services
{
    public interface IDepartmentService
    {
        int CreateDepartment(CreateDepartmentDto createDepartmentDto);
        IEnumerable<DepartmentDTO> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentByID(int id);
        int UpdateDepartment(CreateDepartmentDto createDepartmentDto);
    }
}