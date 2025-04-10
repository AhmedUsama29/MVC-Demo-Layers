using Demo.DataAccess.Models.SharedModels;

namespace Demo.DataAccess.Models.DepartmentModels
{
    public class Department : BaseEntity
    {

        public string Name { get; set; } = null!;
        public int Code { get; set; }

        public string? Description { get; set; }

    }
}
