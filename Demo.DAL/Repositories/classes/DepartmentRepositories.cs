using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Data.Contexts;
using Demo.DAL.Models.DepartmentModel;
using Demo.DAL.Repositories.Interfaces;

namespace Demo.DAL.Repositories.classes
{
    public class DepartmentRepository(ApplicationDbContext _dbContext) :GenericRepository<Department>(_dbContext), IDepartmentRepository
    {
        
    }
}
