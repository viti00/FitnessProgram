namespace FitnessProgram.Controllers
{
    using FitnessProgram.Models.Partners;
    using FitnessProgram.Services.PartnerService;
    using Microsoft.AspNetCore.Authorization;
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
            var allPosts = partnerService.GetAll(query.CurrentPage, AllPartnersQueryModel.PostPerPage);

            return View(allPosts);
        }
    }
}
