using AutoMapper;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using Demo.BusinessLogic.DTOs.RolesDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModels;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Classes
{
    public class RolesServices(RoleManager<IdentityRole> _roleManager,
                                IMapper _mapper,
                                IUnitOfWork _unitOfWork) : IRolesServices
    {

        public IEnumerable<GetRolesDto> GetAllRoles(string? RoleSearchName)
        {

            IEnumerable<IdentityRole> Roles;

            if (string.IsNullOrEmpty(RoleSearchName))
            {
                Roles = _roleManager.Roles;
            }
            else
            {
                Roles = _roleManager.Roles.Where(e => (e.Name.ToLower()).Contains(RoleSearchName.ToLower()));
            }

            return _mapper.Map<IEnumerable<GetRolesDto>>(Roles);
        }

        //public EmployeeDetailsDto? GetEmployeeByID(int id)
        //{
        //    var emp = _unitOfWork.EmployeeRepository.GetById(id);

        //    return emp is null ? null
        //        : _mapper.Map<EmployeeDetailsDto>(emp);
        //}

        //public int CreateEmployee(CreateEmployeeDto createemployeeDto)
        //{

        //    var emp = _mapper.Map<Employee>(createemployeeDto);

        //    var imageName = _AttatchmentService.Upload(createemployeeDto.Image, "Images");

        //    emp.ImageName = imageName;

        //    _unitOfWork.EmployeeRepository.Add(emp);
        //    return _unitOfWork.SaveChanges();
        //}


        //public int UpdateEmployee(UpdateEmployeeDto updateemployeeDto)
        //{


        //    var emp = _mapper.Map<Employee>(updateemployeeDto);

        //    if (updateemployeeDto.Image != null)
        //    {
        //        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images");
        //        var filePath = Path.Combine(folderPath, updateemployeeDto.Image.Name);
        //        _AttatchmentService.Delete(filePath);
        //        var imageName = _AttatchmentService.Upload(updateemployeeDto.Image, "Images");
        //        emp.ImageName = imageName;
        //    }
        //    else
        //    {
        //        emp.ImageName = updateemployeeDto.ImageName;
        //    }

        //    _unitOfWork.EmployeeRepository.Update(emp);
        //    return _unitOfWork.SaveChanges();
        //}

        //public bool DeleteEmployee(int id)
        //{
        //    var emp = _unitOfWork.EmployeeRepository.GetById(id);
        //    if (emp is null) return false;
        //    else
        //    {
        //        emp.IsDeleted = true;
        //        _unitOfWork.EmployeeRepository.Update(emp);
        //        return (_unitOfWork.SaveChanges() > 0) ? true : false;
        //    }
        //}

    }
}
