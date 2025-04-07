using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTO.EmployeeDtos;

namespace Demo.BLL.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees();

        EmployeeDetailsDto GetEmployeebyId(int id);

        int CreateEmployee(CreatedEmployeeDto employeeDto);

        int UpdatedEmployee(UpdatedEmployeeDto employeeDto);

        bool DeleteEmployee(int id);
    }
}
