using Microsoft.AspNetCore.Mvc;

namespace FitnessProgram.Controllers
{
    public class Customers : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
