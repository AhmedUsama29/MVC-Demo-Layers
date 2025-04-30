using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DTOs.UserDtos
{
    public class GetUserDto
    {

        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public IEnumerable<string> Roles { get; set; }

    }
}
