namespace FitnessProgram.Controllers
{
    using FitnessProgram.Infrastructure;
    using FitnessProgram.ViewModels.Partner;
    using FitnessProgram.Services.PartnerService;
    using Microsoft.AspNetCore.Mvc;
    public class PartnersController : Controller
    {
        private readonly IPartnerService partnerService;

        public PartnersController(IPartnerService partnerService)
        {
            this.partnerService = partnerService;
        }

        public IActionResult All([FromQuery] AllPartnersQueryModel query)
        {
            var isAdministrator = User.IsAdministrator();

            var allPosts = partnerService.GetAll(query.CurrentPage, AllPartnersQueryModel.PostPerPage, query, isAdministrator);

            return View(allPosts);
        }
    }
}
