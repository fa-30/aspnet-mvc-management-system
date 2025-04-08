using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTO.DepartmentDtos;
using Demo.BLL.Factories;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Repositories.Interfaces;


namespace Demo.BLL.Services.classes
{
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return departments.Select(D => D.ToDepartmentDto());
        }

        public DepartmentDetialsDto GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            return department is null ? null : department.ToDepartmentDetialsDto();

        }
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteDepartment(int id)
        {
            var Department = _unitOfWork.DepartmentRepository.GetById(id);
            if (Department is null) return false;
            else
            {
                _unitOfWork.DepartmentRepository.Remove(Department);
                int Result=_unitOfWork.SaveChanges();
                if (Result> 0) return true;
                else return false;
            }
        }

        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }
    }
}
