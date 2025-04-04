
using Demo.DAL.Models.DepartmentModel;

namespace Demo.DAL.Repositories
{
    public interface IDepartmentRepository
    {
        int Add(Department department);
        IEnumerable<Department> GetAll(bool WithTracking = false);
        Department? GetById(int id);
        int Remove(Department department);
        int update(Department department);
    }
}