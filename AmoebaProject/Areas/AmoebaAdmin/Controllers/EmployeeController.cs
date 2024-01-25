using Microsoft.AspNetCore.Mvc;

namespace AmoebaProject.Areas.AmoebaAdmin.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
