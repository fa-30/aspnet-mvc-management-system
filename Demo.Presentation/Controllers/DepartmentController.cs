using Demo.BLL.Services;

using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService) : Controller
    {

        public IActionResult Index()
        {
            var Department = _departmentService.GetAllDepartments();
            return View();
        }

    }
}
