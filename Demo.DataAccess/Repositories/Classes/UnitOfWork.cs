using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IEmployeeRepository> _EmployeeRepository;
        private readonly Lazy<IDepartmentRepository> _DepartmentRepository;
        private readonly AppDBContext _AppDBContext;

        public UnitOfWork(AppDBContext _appDBContext)
        {
            _AppDBContext = _appDBContext;
            _EmployeeRepository = new Lazy<IEmployeeRepository>(()=> new EmployeeRepository(_AppDBContext));
            _DepartmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_AppDBContext));
        }

        public IEmployeeRepository EmployeeRepository => _EmployeeRepository.Value;

        public IDepartmentRepository DepartmentRepository => _DepartmentRepository.Value;

        public int SaveChanges()
        {
            return _AppDBContext.SaveChanges();
        }
    }
}
