namespace FitnessProgram.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static FitnessProgram.WebConstants;

    public class BestResults : Controller
    {
        public IActionResult All()
        {
            return View();
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
