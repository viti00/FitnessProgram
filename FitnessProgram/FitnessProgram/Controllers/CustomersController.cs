namespace FitnessProgram.Controllers
{
    using FitnessProgram.ViewModels.Customer;
    using FitnessProgram.Services.CustomerService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using FitnessProgram.Infrastructure;

    public class CustomersController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
            => this.customerService = customerService;

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CustomerFormModel model)
        {
            customerService.BecomeCustomer(model, User.GetId());

            return RedirectToAction("Index", "Home");
        }
    }
}
