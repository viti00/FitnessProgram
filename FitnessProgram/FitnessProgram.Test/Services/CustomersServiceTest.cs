namespace FitnessProgram.Test.Services
{
    using FitnessProgram.Services.CustomerService;
    using FitnessProgram.Test.Mocks;
    using FitnessProgram.ViewModels.Customer;

    public class CustomersServiceTest
    {
        [Fact]
        public void BecomeCustomerShoudAddNewCustomerToDatabaseIfUserExists()
        {
            int expected = 1;
            using var data = DatabaseMock.Instance;
            var customerService = new CustomerService(data);
            data.Users.Add(GetUser());
            data.SaveChanges();
            var userId = data.Users.First().Id;
            var newCustomer = GetCustomer();


            customerService.BecomeCustomer(newCustomer, userId);

            Assert.Equal(expected, data.Customers.Count());
        }
        [Fact]
        public void BecomeCustomerShoudNotAddNewCustomerToDatabaseIfUserNotExists()
        {
            int expected = 0;
            using var data = DatabaseMock.Instance;
            var customerService = new CustomerService(data);
            var newCustomer = GetCustomer();


            customerService.BecomeCustomer(newCustomer, null);

            Assert.Equal(expected, data.Customers.Count());
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(2, false)]
        public void ApproveShoudReturnTrueIfCustomerIsSuccsessfulyApprovedAndFalseIfNot(int id, bool expectedResult)
        {
            using var data = DatabaseMock.Instance;
            var customerService = new CustomerService(data);
            data.Users.Add(GetUser());
            data.SaveChanges();
            var userId = data.Users.First().Id;
            var newCustomer = GetCustomer();
            customerService.BecomeCustomer(newCustomer, userId);

            var result = customerService.Approve(id);

            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedResult, data.Customers.First().IsApproved);
        }

        [Theory]
        [InlineData(1, true, 0)]
        [InlineData(2, false, 1)]
        public void RejectShoudReturnTrueAndRemoveCustomerFromDatabaseIfExistsAndShoudReturnFalseIfNotExists(int id, bool expectedResult, int customersCountInDb)
        {
            using var data = DatabaseMock.Instance;
            var customerService = new CustomerService(data);
            data.Users.Add(GetUser());
            data.SaveChanges();
            var userId = data.Users.First().Id;
            var newCustomer = GetCustomer();
            customerService.BecomeCustomer(newCustomer, userId);

            var result = customerService.Reject(id);

            Assert.IsType<bool>(result);
            Assert.Equal(customersCountInDb, data.Customers.Count());
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GetApprovedShoudReturnOnlyAprrovedCustomers()
        {
            int expectedInDB = 9;
            int expected = 5;
            using var data = DatabaseMock.Instance;
            var customerService = new CustomerService(data);
            data.Customers.AddRange(GetCustomers());
            data.SaveChanges();

            var result = customerService.GetApproved();

            Assert.NotNull(result);
            Assert.IsType<List<CustomerViewModel>>(result);
            Assert.Equal(expectedInDB, data.Customers.Count());
            Assert.Equal(expected, result.Count);

        }
        [Fact]
        public void GetAwaitingApprovalShoudReturnOnlyNotAprrovedCustomers()
        {
            int expectedInDB = 9;
            int expected = 4;
            using var data = DatabaseMock.Instance;
            var customerService = new CustomerService(data);
            data.Customers.AddRange(GetCustomers());
            data.SaveChanges();

            var result = customerService.GetAwaitingApproval();

            Assert.NotNull(result);
            Assert.IsType<List<CustomerViewModel>>(result);
            Assert.Equal(expectedInDB, data.Customers.Count());
            Assert.Equal(expected, result.Count);

        }
    }
}
