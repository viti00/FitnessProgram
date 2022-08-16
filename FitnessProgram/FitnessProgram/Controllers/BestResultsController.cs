namespace FitnessProgram.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class BestResultsController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
