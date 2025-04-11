using Demo.DataAccess.Models.DepartmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DTOs.DepartmentDtos
{
    public class DepartmentDetailsDto
    {

        public DepartmentDetailsDto(Department d)
        {
            id = d.Id;
            Name = d.Name;
            Code = d.Code;
            Description = d.Description;
            DateOfCreation = d.CreatedOn;
            CreatedBy = d.CreatedBy;
            LastModifiedBy = d.LastModifiedBy;
            IsDeleted = d.IsDeleted;
            LastModifiedOn = d.LastModifiedOn;
        }

        public int id { get; set; }

        public string Name { get; set; }

        public int Code { get; set; }

        public string? Description { get; set; }

        public DateTime? DateOfCreation { get; set; }

        public int CreatedBy { get; set; }

        public int LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }


        public bool IsDeleted { get; set; }

    }
}
