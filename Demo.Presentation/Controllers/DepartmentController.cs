using Demo.BLL.DTO;
using Demo.BLL.Services;
using Demo.Presentation.ViewModels.DepartmentViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService, ILogger<DepartmentController> _logger, IWebHostEnvironment _environment) : Controller
    {

        public IActionResult Index()
        {
            var Department = _departmentService.GetAllDepartments();
            return View(Department);
        }

        #region  create Department

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    int Result = _departmentService.AddDepartment(departmentDto);
                    if (Result > 0)
                        // return View(viewName: nameof(Index), _departmentService.GetAllDepartments()); // XXXXXXXX
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(key: string.Empty, errorMessage: "Department Can't Be Created");

                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        // 1. Development => Log Error In Console and Return Same View With Error Message
                        ModelState.AddModelError(key: string.Empty, errorMessage: ex.Message);

                    }
                    else
                    {
                        // 2. Deployment => Log Error In File | Table in Database And Return Error View
                        _logger.LogError(message: ex.Message);

                    }
                }

            }
            return View(model: departmentDto);
        }
        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            return View(department);
        }
        #endregion

        #region edit dep

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            var departmentViewModel = new DepartmentEditViewModel()
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn
            };
            return View(departmentViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id,DepartmentEditViewModel viewModel)
        {
           
            if (!ModelState.IsValid) return View(model: viewModel);
            try
            {
                var UpdatedDepartment = new UpdatedDepartmentDto()
                {
                    Id = id,
                    Code = viewModel.Code,
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    DateOfCreation = viewModel.DateOfCreation
                };
                int Result = _departmentService.UpdateDepartment(UpdatedDepartment);
                if (Result > 0)
                    return RedirectToAction(actionName: nameof(Index));
                else
                {
                    ModelState.AddModelError(key: string.Empty, errorMessage: "Department is not Updated");
                  
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    // 1. Development => Log Error In Console and Return Same View With Error Message
                    ModelState.AddModelError(key: string.Empty, errorMessage: ex.Message);
                   
                }
                else
                {
                    // 2. Deployment => Log Error In File | Table in Database And Return Error View
                    _logger.LogError(message: ex.Message);
                    return View(viewName: "ErrorView", model: ex);
                }
            }
            return View(model: viewModel);      
        }
        #endregion
    }
}
