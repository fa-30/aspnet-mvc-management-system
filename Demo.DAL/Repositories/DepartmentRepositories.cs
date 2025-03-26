using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Data.Contexts;

namespace Demo.DAL.Repositories
{
    public class DepartmentRepository(ApplicationDbContext _dbContext) : IDepartmentRepository
    {
        //public readonly ApplicationDbContext dbContext = DbContext;

        public IEnumerable<Department> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                return _dbContext.Departments.ToList();
            else
                return _dbContext.Departments.AsNoTracking().ToList();
        }
        public Department? GetById(int id)
        {
            return _dbContext.Departments.Find(id);
        }

        public int update(Department department)
        {
            _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges();
        }

        public int Remove(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }

        public int Add(Department department)
        {
            _dbContext.Add(department);
            return _dbContext.SaveChanges();
        }

    }
}
