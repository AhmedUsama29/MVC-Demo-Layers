using Demo.DataAccess.Models.DepartmentModels;
using Demo.DataAccess.Models.EmployeeModels;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Demo.DataAccess.Contexts
{
    public class AppDBContext : DbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //}



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
