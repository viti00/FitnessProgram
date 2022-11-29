namespace FitnessProgram.Services.CustomerService
{
    using FitnessProgram.Data;
    using FitnessProgram.Data.Models;
    using FitnessProgram.ViewModels.Customer;
    using System.Collections.Generic;

    public class CustomerService : ICustomerService
    {
        private readonly FitnessProgramDbContext context;

        public CustomerService(FitnessProgramDbContext context)
            => this.context = context;

        public void BecomeCustomer(CustomerFormModel model, string userId)
        {
            if(userId != null)
            {
                var customer = new Customer
                {
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Sex = model.Sex,
                    Age = model.Age,
                    DesiredResults = model.DesiredResults,
                    IsApproved = false,
                    UserId = userId
                };

                context.Customers.Add(customer);
                context.SaveChanges();
            }
        }

        public List<CustomerViewModel> GetApproved()
        {
            var approved = context.Customers
                .Where(x => x.IsApproved)
                .Select(x => new CustomerViewModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber,
                    Sex = x.Sex,
                    Age = x.Age,
                    DesiredResults = x.DesiredResults,
                    Email = x.User.Email,
                    IsApproved = x.IsApproved
                })
                .ToList();

            return approved;
        }

        public List<CustomerViewModel> GetAwaitingApproval()
        {
            
            var awaiting = context.Customers
                .Where(x => x.IsApproved == false)
                .Select(x => new CustomerViewModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber,
                    Sex = x.Sex,
                    Age = x.Age,
                    DesiredResults = x.DesiredResults,
                    Email = x.User.Email,
                    IsApproved = x.IsApproved
                }).ToList();

            return awaiting;
        }

        public bool Approve(int customerId)
        {
            var customer = GetCustomerById(customerId);

            if(customer == null)
            {
                return false;
            }

            customer.IsApproved = true;

            context.SaveChanges();

            return true;
        }

        public bool Reject(int customerId)
        {
            var customer = GetCustomerById(customerId);

            if(customer == null)
            {
                return false;
            }

            context.Customers.Remove(customer);
            context.SaveChanges();

            return true;
        }

        private Customer GetCustomerById(int id)
            => context.Customers.FirstOrDefault(x => x.Id == id);
    }
}
