using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.BLL.DTO.EmployeeDtos;
using Demo.BLL.Services.AttachementService;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Repositories.Interfaces;


namespace Demo.BLL.Services.classes
{
    public class EmployeeService(IUnitOfWork _unitOfWork, IMapper _mapper , IAttachmentService _attachmentService) : IEmployeeService
    {
 
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            //var employeesDto = _employeeRepository.GetAll(selector: Emp => new EmployeeDto()
            //{
            //    Id = Emp.Id,
            //    Name = Emp.Name,
            //    Age = Emp.Age,
            //    Email = Emp.Email,
            //    IsActive = Emp.IsActive,
            //    Salary = Emp.Salary,
            //    EmpType = Emp.EmployeeType.ToString(),
            //    EmpGender = Emp.Gender.ToString()
            //});//.Where(predicate: E => E.Age > 25);

            //return employeesDto;
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
                employees = _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = _unitOfWork.EmployeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(source: employees);
            return employeesDto;


            //var employeesDto = Employees.Select(selector: Emp => new EmployeeDto()
            //{
            //    Id = Emp.Id,
            //    Name = Emp.Name,
            //    Age = Emp.Age,
            //    Email = Emp.Email,
            //    IsActive = Emp.IsActive,
            //    Salary = Emp.Salary,
            //    EmployeeType = Emp.EmployeeType.ToString(),
            //    Gender = Emp.Gender.ToString()
            //});
            //return employeesDto;
        }
        
       
        public EmployeeDetailsDto? GetEmployeebyId(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<Employee, EmployeeDetailsDto>(source: employee);
            //return employee is null ? null : new EmployeeDetailsDto()
            //{
            //    Id = employee.Id,
            //    Name = employee.Name,
            //    Salary = employee.Salary,
            //    Address = employee.Address,
            //    Age = employee.Age,
            //    Email = employee.Email,
            //    HiringDate = DateOnly.FromDateTime(employee.HiringDate),
            //    IsActive = employee.IsActive,
            //    PhoneNumber = employee.PhoneNumber,
            //    EmployeeType = employee.EmployeeType.ToString(),
            //    Gender = employee.Gender.ToString(),
            //    CreatedBy = 1,
            //    CreatedOn = employee.CreatedOn,
            //    LastModifiedBy = 1,
            //    LastModifiedOn = employee.LastModifiedOn
            //};
        }



        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(source: employeeDto);
            if (employeeDto.Image is not null)
            {
                employee.ImageName = _attachmentService.Upload(file: employeeDto.Image, FolderName: "Images");
            }
            _unitOfWork.EmployeeRepository.Add(employee);
             return _unitOfWork.SaveChanges();  
        }

    
        public int UpdatedEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto);
            if (employeeDto.Image is not null)
            {
                employee.ImageName = _attachmentService.Upload(file: employeeDto.Image, FolderName: "Images");
            }
            _unitOfWork.EmployeeRepository.Update(employee);
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                _unitOfWork.EmployeeRepository.Update(employee);
                return _unitOfWork.SaveChanges()> 0 ? true : false;
            }
        }

    }
}
