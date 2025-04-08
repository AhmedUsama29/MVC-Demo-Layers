using Demo.BusinessLogic.DTOs;
using Demo.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Factories
{
    public static class DepartmentFactory
    {

        public static Department ToEntity(this CreateDepartmentDto dto)
        {

            return new Department
            {
                Code = dto.Code,
                Name = dto.Name,
                Description = dto.Description,
                CreatedOn = dto.DateOfCreation,
            };

        }

    }
}
