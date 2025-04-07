using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Data.Contexts;
using Demo.DAL.Repositories.Interfaces;

namespace Demo.DAL.Repositories.classes
{
    public class UnitOfWork : IUnitOfWork , IDisposable
    {
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork( ApplicationDbContext dbContext) { 
        
          _dbContext = dbContext;
          _departmentRepository= new Lazy<IDepartmentRepository>(()=> new DepartmentRepository(dbContext));
          _employeeRepository = new Lazy<IEmployeeRepository>(()=> new EmployeeRepository(dbContext));
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public int SaveChanges()
        {
          return  _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
