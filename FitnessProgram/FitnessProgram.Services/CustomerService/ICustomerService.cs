namespace FitnessProgram.Services.CustomerService
{
    using FitnessProgram.ViewModels.Customer;

    public interface ICustomerService
    {
        public bool BecomeCustomer(CustomerFormModel model, string userId);

        public List<CustomerViewModel> GetApproved();

        public List<CustomerViewModel> GetAwaitingApproval();

        public bool Approve(int customerId);

        public bool Reject(int customerId);
    }
}
