﻿namespace FitnessProgram.Areas.Admin.Controllers
{
    using FitnessProgram.ViewModels.BestResult;
    using FitnessProgram.Infrastructure;
    using FitnessProgram.Services.BestResultService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static WebConstants;

    [Area(AdminConstants.AreaName)]
    public class BestResultsController : Controller
    {
        private readonly IBestResultService bestResultService;

        public BestResultsController(IBestResultService bestResultService)
            => this.bestResultService = bestResultService;

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult Add(BestResultFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bestResultService.AddBestResult(model);

            return Redirect("https://localhost:7238/BestResults/All");
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var bestResult = bestResultService.CreateEditModel(id);

            if(bestResult == null)
            {
                return BadRequest();
            }

            return View(bestResult);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult Edit(int id, BestResultFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bestResultService.EditBestResult(id, model);

            return Redirect($"https://localhost:7238/bestresults/details/{id}");
        }

        [Authorize(Roles =AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            var bestResult = bestResultService.GetBestResultById(id);

            if (!User.IsAdministrator())
            {
                return Unauthorized();
            }

            if(bestResult == null)
            {
                return BadRequest();
            }

            bestResultService.DeleteBestResult(bestResult);

            return Redirect("https://localhost:7238/BestResults/all");
        }

    }
}
