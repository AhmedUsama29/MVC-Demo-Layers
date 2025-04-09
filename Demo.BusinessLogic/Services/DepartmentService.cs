using Demo.BusinessLogic.DTOs;
using Demo.BusinessLogic.Factories;
using Demo.DataAccess.Models;
using Demo.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    {

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {

            var departments = _departmentRepository.GetAll();

            var DepartmentsToReturn = departments.Select(d => new DepartmentDto
            {

                id = d.Id,
                Name = d.Name,
                Code = d.Code,
                Description = d.Description,
                DateOfCreation = d.CreatedOn
            });

            return DepartmentsToReturn;

        }


        public DepartmentDetailsDto? GetDepartmentByID(int id)
        {

            var department = _departmentRepository.GetById(id);
            return department is null ? null : new DepartmentDetailsDto(department);

        }

        public int CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {
            var res = _departmentRepository.Add(createDepartmentDto.ToEntity());
            return res;
        }

        public int UpdateDepartment(UpdateDepartmentDto updateDepartmentDto)
        {
            var res = _departmentRepository.Update(updateDepartmentDto.ToEntity());
            return res;
        }

    }
}
