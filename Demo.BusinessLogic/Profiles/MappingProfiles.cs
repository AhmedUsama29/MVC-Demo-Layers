﻿using AutoMapper;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using Demo.DataAccess.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Profiles
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            // CreateMap<Source, Destination>();
            CreateMap<Employee, GetEmployeeDto>()
            .ForMember(dest => dest.EmpType, option => option.MapFrom(emp => emp.EmployeeType))
            .ForMember(dest => dest.EmpGender, option => option.MapFrom(emp => emp.Gender))
            .ForMember(dest => dest.Department, option => option.MapFrom(emp =>emp.Department != null ? emp.Department.Name : null));

            CreateMap<Employee, EmployeeDetailsDto>()
            .ForMember(dest => dest.EmployeeType, option => option.MapFrom(emp => emp.EmployeeType))
            .ForMember(dest => dest.Gender, option => option.MapFrom(emp => emp.Gender))
            .ForMember(dest => dest.HiringDate, option => option.MapFrom(emp => DateOnly.FromDateTime(emp.HiringDate)))
            .ForMember(dest => dest.Department, option => option.MapFrom(emp => emp.Department != null ? emp.Department.Name : null));

            CreateMap<CreateEmployeeDto,Employee>()
            .ForMember(dest => dest.HiringDate , option => option.MapFrom(empDto => empDto.HiringDate.ToDateTime(TimeOnly.MinValue)));
            
            CreateMap<UpdateEmployeeDto,Employee>()
            .ForMember(dest => dest.HiringDate, option => option.MapFrom(empDto => empDto.HiringDate.ToDateTime(TimeOnly.MinValue)));


        }

    }
}
