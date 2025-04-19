using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models;
using Demo.DataAccess.Repositories;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {

            var departments = _unitOfWork.DepartmentRepository.GetAll();

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

            var department = _unitOfWork.DepartmentRepository.GetById(id);
            return department is null ? null : new DepartmentDetailsDto(department);

        }

        public int CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {
            _unitOfWork.DepartmentRepository.Add(createDepartmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }

        public int UpdateDepartment(UpdateDepartmentDto updateDepartmentDto)
        {
            _unitOfWork.DepartmentRepository.Update(updateDepartmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department is null) return false;
            else
            {
                _unitOfWork.DepartmentRepository.Remove(department);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }

    }
}
