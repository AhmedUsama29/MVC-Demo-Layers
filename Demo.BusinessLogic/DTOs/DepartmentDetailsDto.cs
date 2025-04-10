using Demo.DataAccess.Models.DepartmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DTOs
{
    public class DepartmentDetailsDto
    {

        public DepartmentDetailsDto(Department d)
        {
            this.id = d.Id;
            this.Name = d.Name;
            this.Code = d.Code;
            this.Description = d.Description;
            this.DateOfCreation = d.CreatedOn;
            this.CreatedBy = d.CreatedBy;
            this.LastModifiedBy = d.LastModifiedBy;
            this.IsDeleted = d.IsDeleted;
            this.LastModifiedOn = d.LastModifiedOn;
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
