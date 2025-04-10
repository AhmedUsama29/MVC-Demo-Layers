using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class EmployeeRepository(AppDBContext _dbContext) : GenericRepository<Employee>(_dbContext),
                                                                  IEmployeeRepository
    {

    }
}
