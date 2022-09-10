namespace FitnessProgram.Services.CustomerService
{
    using FitnessProgram.Areas.Admin.Models.Customers;
    using FitnessProgram.Models.Customer;

    public interface ICustomerService
    {
        public void BecomeCustomer(CustomerFormModel model, string userId);

        public List<CustomerViewModel> GetApproved();

        public List<CustomerViewModel> GetAwaitingApproval();

        public bool Approve(int customerId);

        public bool Reject(int customerId);
    }
}
