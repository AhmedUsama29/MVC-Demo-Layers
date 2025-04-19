using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IDepartmentRepository _DepartmentRepository;
        private readonly AppDBContext _AppDBContext;

        public UnitOfWork(IEmployeeRepository _employeeRepository,
                          IDepartmentRepository _departmentRepository,
                          AppDBContext _appDBContext)
        {
            _EmployeeRepository = _employeeRepository;
            _DepartmentRepository = _departmentRepository;
            _AppDBContext = _appDBContext;
        }

        public IEmployeeRepository EmployeeRepository => _EmployeeRepository;

        public IDepartmentRepository DepartmentRepository => _DepartmentRepository;

        public int SaveChanges()
        {
            return _AppDBContext.SaveChanges();
        }
    }
}
