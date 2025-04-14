using Demo.BLL.DTO.EmployeeDtos;
using System;
using Demo.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Models.Shared.Enums;
using Microsoft.Extensions.Hosting;
using Demo.BLL.DTO.DepartmentDtos;
using Demo.Presentation.ViewModels.DepartmentViewModel;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService, ILogger<DepartmentController> _logger, IWebHostEnvironment _environment ) : Controller
    {
        [Authorize]
        public IActionResult Index(string? EmployeeSearchName)
        {

            var Employees = _employeeService.GetAllEmployees( EmployeeSearchName);
            return View(Employees);
        }
        public IActionResult Create()//[FromServices] IDepartmentService departmentService)
        {
            //ViewData["Departments"] = departmentService.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    var employeeDto = new CreatedEmployeeDto()
                    {
                        Name = employee.Name,
                        Salary = employee.Salary,
                        Address = employee.Address,
                        Age = employee.Age,
                        Email = employee.Email,
                        PhoneNumber = employee.PhoneNumber,
                        IsActive = employee.IsActive,
                        HiringDate = employee.HiringDate,
                        Gender = employee.Gender,
                        EmployeeType = employee.EmployeeType,
                        DepartmentId = employee.DepartmentId,
                        Image=employee.Image,
                    };
                    int Result = _employeeService.CreateEmployee(employeeDto);
                    if (Result > 0)
                        return RedirectToAction(actionName: nameof(Index));
                    else
                    {
                        ModelState.AddModelError(key: string.Empty, errorMessage: "Can't Create Employee");
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                        ModelState.AddModelError(key: string.Empty, errorMessage: ex.Message);
                    else
                        _logger.LogError(ex.Message);
                }
            }
            return View(employee);
        }

        #region Details Of Employee
        [HttpGet]

        public ActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeebyId(id.Value);
            return employee is null ? NotFound() : View(employee);
        }
        #endregion

        #region Edit Employee  
        [HttpGet]

        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeebyId(id: id.Value);
            if (employee is null) return NotFound();

            var employeeDto = new EmployeeViewModel()
            {

                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(value: employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(value: employee.EmployeeType),
                DepartmentId = employee.DepartmentId,
            };
            return View(employeeDto);

         }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeViewModel)
        {
            if (!id.HasValue ) return BadRequest();
            if (!ModelState.IsValid) return View(employeeViewModel);
            try
            {
                var employeeDto = new UpdatedEmployeeDto()
                {
                    Id = id.Value,
                    Name = employeeViewModel.Name,
                    Address = employeeViewModel.Address,
                    Age = employeeViewModel.Age,
                    Email = employeeViewModel.Email,
                    EmployeeType = employeeViewModel.EmployeeType,
                    Gender = employeeViewModel.Gender,
                    HiringDate = employeeViewModel.HiringDate,
                    IsActive = employeeViewModel.IsActive,
                    PhoneNumber = employeeViewModel.PhoneNumber,
                    Salary = employeeViewModel.Salary,
                    DepartmentId = employeeViewModel.DepartmentId,
                    Image= employeeViewModel.Image,
                };
                var Result = _employeeService.UpdatedEmployee(employeeDto);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError( string.Empty,  "Employee is not Updated");
                    return View(employeeViewModel);
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError( string.Empty,  ex.Message); 
                    return View(employeeViewModel);
                }
                else
                {
                    _logger.LogError(message: ex.Message);
                    return View("ErrorView",ex);
                }
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                var Deleted = _employeeService.DeleteEmployee(id);
                if (Deleted)
                {
                    return RedirectToAction(actionName: nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(key: string.Empty, errorMessage: "Employee is not Deleted");
                    return RedirectToAction(actionName: nameof(Delete), routeValues: new { id = id });
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    return RedirectToAction(actionName: nameof(Index));
                    // With Message That Department Not Deleted
                }
                else
                {
                    _logger.LogError(message: ex.Message);
                    return View(viewName: "Error", model: ex);
                }
            }
        }
        #endregion
    }
}
