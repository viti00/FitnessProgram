using FitnessProgram.Services.BestResultService;
using FitnessProgram.Services.CommentService;
namespace FitnessProgram.Infrastructure
{
    using FitnessProgram.Services.CustomerService;
    using FitnessProgram.Services.LikeService;
    using FitnessProgram.Services.PartnerService;
    using FitnessProgram.Services.PostServices;

    public static class FitnessProgramApiServiceCollectionExtension 
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IBestResultService, BestResultService>();
            services.AddScoped<IPartnerService, PartnerService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddSignalR(e =>
            {
                e.MaximumReceiveMessageSize = 102400000;
            });
            services.AddMemoryCache();

            return services;
        }
    }
}
