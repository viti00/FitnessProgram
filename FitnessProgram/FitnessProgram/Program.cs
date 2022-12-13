using FitnessProgram.Infrastructure;
using FitnessProgram.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FitnessProgram.Services.PostServices;
using FitnessProgram.Data.Models;
using Microsoft.AspNetCore.Mvc;
using FitnessProgram.Services.LikeService;
using FitnessProgram.Services.CommentService;
using FitnessProgram.Services.BestResultService;
using FitnessProgram.Services.PartnerService;
using FitnessProgram.Controllers.Hubs;
using FitnessProgram.Services.CustomerService;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("FitnessProgramDbContextConnection");
builder.Services.AddDbContext<FitnessProgramDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FitnessProgramDbContext>();

builder.Services.AddControllersWithViews(options=>
{
    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});


builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IBestResultService, BestResultService>();
builder.Services.AddScoped<IPartnerService, PartnerService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddSignalR(e=>
{
    e.MaximumReceiveMessageSize = 102400000;
});
builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}



app.PrepareDatabase();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<LikesHub>("/likesHub");
app.MapHub<CommentsHub>("/commentsHub");

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllerRoute(
        name: "Areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );

    endpoint.MapDefaultControllerRoute();
});

app.MapRazorPages();

app.Run();
