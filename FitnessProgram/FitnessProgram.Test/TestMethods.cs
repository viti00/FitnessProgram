namespace FitnessProgram.Test
{
    using FitnessProgram.Data.Models;
    using FitnessProgram.ViewModels.Customer;
    using FitnessProgram.ViewModels.Post;
    using Microsoft.Extensions.Caching.Memory;

    public static class TestMethods
    {
        public static IEnumerable<BestResult> GetBestResults()
             => Enumerable.Range(0, 10).Select(x => new BestResult { Story = "Test Story Text" });

        public static IEnumerable<Partner> GetPartners()
            => Enumerable.Range(0, 10)
                         .Select(x => new Partner
                         {
                             Name = "Test Name",
                             Description = "Test Description",
                             PromoCode = "test",
                             Url = "",
                             Photo = new PartnerPhoto
                             {
                                 Bytes = new byte[1024],
                                 Description = "",
                                 FileExtension = ""
                             }
                         });

        public static MemoryCache GetMemoryCache()
        {
            var options = new MemoryCacheOptions();
            var cache = new MemoryCache(options);

            return cache;
        }

        public static IEnumerable<Post> GetPosts()
            => Enumerable.Range(0, 10).Select(x => new Post { Title = "Test Posts", Text = "Test Text", Creator= GetUser() });

        public static User GetUser()
            => new User { Email = "test@abv.bg", UserName = "test", PasswordHash = "123456" };

        public static IEnumerable<Comment> GetComments(string postId)
            => Enumerable.Range(0, 10).Select(x => new Comment { PostId = postId, CreatorId = "", Message = "Test Comments" });

        public static CustomerFormModel GetCustomer()
            => new CustomerFormModel { FullName = "Pesho Peshev", Age = 20, DesiredResults = "I want to be very strong"};

        public static IEnumerable<User> GetUsers()
            => Enumerable.Range(0, 10).Select(x=> new User { Email = $"test@abv.bg{x}", UserName = $"test{x}", PasswordHash = "123456" });

        public static IEnumerable<Customer> GetCustomers()
            => Enumerable.Range(0,9).Select(x => new Customer { FullName = "Pesho Peshev",Age = 20, DesiredResults = "I want to be very strong", User = GetUser(), IsApproved = x%2 == 0?true:false});

        public static PostFormModel GetPost()
            => new PostFormModel { Text = "New Text", Title = "New Title"};

    }
}
