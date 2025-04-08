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

        public IEnumerable<DepartmentDTO> GetAllDepartments()
        {

            var departments = _departmentRepository.GetAll();

            var DepartmentsToReturn = departments.Select(d => new DepartmentDTO
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

        public int UpdateDepartment(CreateDepartmentDto createDepartmentDto)
        {
            var res = _departmentRepository.Update(createDepartmentDto.ToEntity());
            return res;
        }

    }
}
