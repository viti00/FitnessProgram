namespace FitnessProgram.Controllers
{
    using FitnessProgram.Models.BestResult;
    using FitnessProgram.Services.BestResultService;
    using Microsoft.AspNetCore.Mvc;

    public class BestResultsController : Controller
    {
        private readonly IBestResultService bestResultService;

        public BestResultsController(IBestResultService bestResultService)
        {
            this.bestResultService = bestResultService;
        }

        public IActionResult All([FromQuery] AllBestResultsViewModel model)
        {
            var allBestResults = bestResultService.GetAll(model.CurrentPage, AllBestResultsViewModel.PostPerPage);

            return View(allBestResults);
        }

        public IActionResult Details(int id)
        {
            var model = bestResultService.GetDetails(id);

            return View(model);
        }
    }
}
