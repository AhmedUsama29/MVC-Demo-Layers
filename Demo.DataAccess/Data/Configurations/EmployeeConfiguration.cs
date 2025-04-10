using Demo.DataAccess.Models.EmployeeModels;
using Demo.DataAccess.Models.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Data.Configurations
{
    public class EmployeeConfiguration : BaseEntityConfiguration<Employee>, IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {

            builder.Property(e => e.Name).HasColumnType("varchar(50)");
            builder.Property(e => e.Address).HasColumnType("varchar(150)");
            builder.Property(e => e.Salary).HasColumnType("decimal(10,2)");
            builder.Property(e => e.Email).HasColumnType("varchar(30)");
            builder.Property(e => e.PhoneNumber).HasColumnType("varchar(11)");

            builder.Property(e => e.Gender).HasConversion(
                valueToAddinDb => valueToAddinDb.ToString(),
                valueToReadFromDB => (Gender)Enum.Parse(typeof(Gender), valueToReadFromDB))
                .HasColumnType("varchar(6)");

            builder.Property(e => e.EmployeeType).HasConversion(
                valueToAddinDb => valueToAddinDb.ToString(),
                valueToReadFromDB => (EmployeeType)Enum.Parse(typeof(EmployeeType), valueToReadFromDB))
                .HasColumnType("varchar(8)");

            base.Configure(builder);
        }
    }
}
