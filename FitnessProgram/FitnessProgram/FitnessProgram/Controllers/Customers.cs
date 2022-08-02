using Microsoft.AspNetCore.Mvc;

namespace FitnessProgram.Controllers
{
    public class Customers : Controller
    {
        public IActionResult Create()
        {
            ViewData["Title"] = "Become Customer";
            return View();
        }
    }
}
