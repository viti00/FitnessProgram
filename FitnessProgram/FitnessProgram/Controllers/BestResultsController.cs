namespace FitnessProgram.Controllers
{
    using FitnessProgram.Infrastructure;
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
            var isAdministator = User.IsAdministrator();

            var currPageBestResults = bestResultService.GetAll(query.CurrentPage, AllBestResultsQueryModel.PostPerPage, isAdministator);

            return View(currPageBestResults);
        }

        public IActionResult Details(int id)
        {
            var model = bestResultService.GetDetails(id);

            return View(model);
        }
    }
}
