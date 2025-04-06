using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.BLL.DTO.EmployeeDtos;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Repositories.Interfaces;


namespace Demo.BLL.Services.classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository, IMapper _mapper) : IEmployeeService
    {
 
        public IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking = false)
        {
            var employeesDto = _employeeRepository.GetAll(selector: Emp => new EmployeeDto()
            {
                Id = Emp.Id,
                Name = Emp.Name,
                Age = Emp.Age,
                Email = Emp.Email,
                IsActive = Emp.IsActive,
                Salary = Emp.Salary,
                EmpType = Emp.EmployeeType.ToString(),
                EmpGender = Emp.Gender.ToString()
            });//.Where(predicate: E => E.Age > 25);

            return employeesDto;
            //var Employees = _employeeRepository.GetAll(withTracking: WithTracking);
            //var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(source: Employees);
            //return employeesDto;

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
            var employee = _employeeRepository.GetById(id);
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
            var employee = _mapper.Map<CreatedEmployeeDto, Employee>(source: employeeDto);
            return _employeeRepository.Add(entity: employee);
        }

    
        public int UpdatedEmployee(UpdatedEmployeeDto employeeDto)
        {
            return _employeeRepository.Update(entity: _mapper.Map<UpdatedEmployeeDto, Employee>(source: employeeDto));
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                return _employeeRepository.Update(entity: employee) > 0 ? true : false;
            }
        }

    }
}
