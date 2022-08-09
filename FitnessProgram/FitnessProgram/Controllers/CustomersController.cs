namespace FitnessProgram.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CustomersController : Controller
    {
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
    }
}
