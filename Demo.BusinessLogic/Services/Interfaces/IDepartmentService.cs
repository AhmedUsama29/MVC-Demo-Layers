using Demo.BusinessLogic.DTOs.DepartmentDtos;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IDepartmentService
    {
        int CreateDepartment(CreateDepartmentDto createDepartmentDto);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentByID(int id);
        int UpdateDepartment(UpdateDepartmentDto updateDepartmentDto);

        bool DeleteDepartment(int id);
    }
}