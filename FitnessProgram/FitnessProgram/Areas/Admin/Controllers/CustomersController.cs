namespace FitnessProgram.Areas.Admin.Controllers
{
    using FitnessProgram.Services.CustomerService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static WebConstants;

    [Area(AdminConstants.AreaName)]
    public class CustomersController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService) 
            => this.customerService = customerService;

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Approved()
        {
            var approved = customerService.GetApproved();
            return View(approved);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult AwaitingApproval()
        {
            var awaiting = customerService.GetAwaitingApproval();
            return View(awaiting);
        }

        [Authorize(Roles =AdministratorRoleName)]
        public IActionResult Approve(int id)
        {
            var isSuccessed = customerService.Approve(id);

            if (!isSuccessed)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Reject(int id)
        {
            var isSuccessed = customerService.Reject(id);

            if (!isSuccessed)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
