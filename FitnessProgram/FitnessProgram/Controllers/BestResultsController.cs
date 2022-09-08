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

        public IActionResult All([FromQuery] AllBestResultsQueryModel query)
        {
            var allBestResults = bestResultService.GetAll(query.CurrentPage, AllBestResultsQueryModel.PostPerPage);

            return View(allBestResults);
        }

        public IActionResult Details(int id)
        {
            var model = bestResultService.GetDetails(id);

            return View(model);
        }
    }
}
