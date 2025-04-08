using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DTOs
{
    public class CreateDepartmentDto
    {

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Code { get; set; }

        public DateTime DateOfCreation { get; set; }
    }
}
