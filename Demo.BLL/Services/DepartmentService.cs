using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTO;
using Demo.BLL.Factories;
using Demo.DAL.Repositories.Interfaces;


namespace Demo.BLL.Services
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    {
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
            return departments.Select(D => D.ToDepartmentDto());
        }

        public DepartmentDetialsDto GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            return department is null ? null : department.ToDepartmentDetialsDto();

        }
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            return _departmentRepository.Add(department);
        }

        public bool DeleteDepartment(int id)
        {
            var Department = _departmentRepository.GetById(id);
            if (Department is null) return false;
            else
            {
                int Result = _departmentRepository.Remove(Department);
                return Result > 0 ? true : false;
            }
        }

        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            return _departmentRepository.Update(departmentDto.ToEntity());
        }
    }
}
