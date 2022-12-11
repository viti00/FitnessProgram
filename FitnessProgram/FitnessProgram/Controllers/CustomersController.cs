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
            if(!customerService.BecomeCustomer(model, User.GetId()))
            {
                ModelState.AddModelError(string.Empty, "You have already submitted a request");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
