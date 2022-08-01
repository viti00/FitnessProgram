using Microsoft.AspNetCore.Mvc;

namespace FitnessProgram.Controllers
{
    public class Partners : Controller
    {
        public IActionResult All()
        {
            ViewData["Title"] = "Partners";
            return View();
        }

        public IActionResult Add()
        {
            ViewData["Title"] = "Add Partners";
            return View();
        }

        public IActionResult Edit()
        {
            ViewData["Title"] = "Edit Partners";
            return View();
        }
    }
}
