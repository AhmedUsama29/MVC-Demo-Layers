using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DTOs.DepartmentDtos
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage = "Name is Required")]

        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Code is Required")]


        public int Code { get; set; }

        public DateTime? DateOfCreation { get; set; }
    }
}
