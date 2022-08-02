using Microsoft.AspNetCore.Mvc;

namespace FitnessProgram.Controllers
{
    public class BestResults : Controller
    {
        public IActionResult All()
        {
            ViewData["Title"] = "Best Results";
            return View();
        }

        public IActionResult Add()
        {
            ViewData["Title"] = "Add Best Result";
            return View();
        }

        public IActionResult Edit()
        {
            ViewData["Title"] = "Edit Best Result";
            return View();
        }
    }
}
