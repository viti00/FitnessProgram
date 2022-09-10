namespace FitnessProgram.Areas.Admin.Controllers
{
    using FitnessProgram.Areas.Admin.Models.Partners;
    using FitnessProgram.Services.PartnerService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static WebConstants;

    [Area(AdminConstants.AreaName)]
    public class PartnersController : Controller
    {
        private readonly IPartnerService partnerService;

        public PartnersController(IPartnerService partnerService) 
            => this.partnerService = partnerService;

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult Add(PartnerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            partnerService.AddPartner(model);

            return Redirect("https://localhost:7238/Partners/all");
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var partner = partnerService.CreateEditModel(id);

            return View(partner);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult Edit(int id, PartnerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            partnerService.EditPartner(id, model);

            return Redirect("https://localhost:7238/Partners/all");
        }

        [Authorize(Roles =AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            var partner = partnerService.GetPartnerById(id);

            if (!User.IsInRole(AdministratorRoleName))
            {
                return Unauthorized();
            }

            if (partner == null)
            {
                return BadRequest();
            }

            partnerService.DeletePartner(partner);

            return Redirect("https://localhost:7238/Partners/all");
        }
    }
}
