

namespace Demo.DataAccess.Models
{
    public class Department : BaseEntity
    {

        public string Name { get; set; } = null!;
        public int Code { get; set; }

        public string? Description { get; set; }

    }
}
