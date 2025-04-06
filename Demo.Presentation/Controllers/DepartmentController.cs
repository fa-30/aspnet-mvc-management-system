using Demo.BLL.DTO;
using Demo.BLL.DTO.DepartmentDtos;
using Demo.BLL.Services.Interfaces;
using Demo.Presentation.ViewModels.DepartmentViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService, ILogger<DepartmentController> _logger, IWebHostEnvironment _environment) : Controller
    {

        public IActionResult Index()
        {
            ViewData[index: "Message"] = new DepartmentDto { Name = "TestViewData" };
            ViewBag.Message = new DepartmentDto() { Name = "TestViewBag" };
            var Department = _departmentService.GetAllDepartments();
            return View(Department);
        }

        #region  create Department

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    var departmentDto = new CreatedDepartmentDto()
                    {
                        Name = departmentViewModel.Name,
                        Code = departmentViewModel.Code,
                        DateOfCreation = departmentViewModel.DateOfCreation,
                        Description = departmentViewModel.Description
                    };
                    int Result = _departmentService.AddDepartment(departmentDto);
                    string Message;
                    if (Result > 0) {
                        Message = $"Department {departmentViewModel.Name} Is Created Successfully"; }
                    else 
                    {
                        Message = $"Department {departmentViewModel.Name} Can Not Be Created";
                    }

                    TempData["Message"] = Message;
                    return RedirectToAction(actionName: nameof(Index));

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
            return View(departmentViewModel);
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
            var departmentViewModel = new DepartmentViewModel()
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn
            };
            return View(departmentViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id,DepartmentViewModel viewModel)
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

        #region  delete
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department is null) return NotFound();
        //    return View(department);
        //}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = _departmentService.DeleteDepartment(id);
                if (Deleted)
                    return RedirectToAction(actionName: nameof(Index));
                else
                {
                    ModelState.AddModelError(key: string.Empty, errorMessage: "Department Is Not Deleted");
                    return RedirectToAction(actionName: nameof(Delete), routeValues: new { id });
                }
            }
            catch(Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    // 1. Development => Log Error In Console and Return Same View With Error Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(actionName: nameof(Index));


                }
                else
                {
                    // 2. Deployment => Log Error In File | Table in Database And Return Error View
                    _logger.LogError(ex.Message);
                    return View("ErrorView" , ex);
                }
            }
        }
        #endregion
    }
}
