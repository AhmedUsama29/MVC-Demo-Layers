using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DTOs
{
    public class DepartmentDTO
    {

        public int id { get; set; }

        public required string Name { get; set; }

        public int Code { get; set; }

        public string? Description { get; set; }

        public DateTime? DateOfCreation { get; set; }

        
    }
}
