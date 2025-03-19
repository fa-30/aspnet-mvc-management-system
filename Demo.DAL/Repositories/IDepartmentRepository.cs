
namespace Demo.DAL.Repositories
{
    internal interface IDepartmentRepository
    {
        int Add(Department department);
        IEnumerable<Department> GetAll(bool WithTracking = false);
        Department? GetById(int id);
        int Remove(Department department);
        int update(Department department);
    }
}