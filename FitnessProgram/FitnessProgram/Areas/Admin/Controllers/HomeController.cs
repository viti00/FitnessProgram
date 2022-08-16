namespace FitnessProgram.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area(AdminConstants.AreaName)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
