namespace FitnessProgram.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static FitnessProgram.WebConstants;
    public class PartnersController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
