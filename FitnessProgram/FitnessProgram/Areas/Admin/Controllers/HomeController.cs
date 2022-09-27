namespace FitnessProgram.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(AdminConstants.AreaName)]
    public class HomeController : Controller
    {
        [Authorize(Roles =WebConstants.AdministratorRoleName)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
