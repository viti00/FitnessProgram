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

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
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


builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<ILikeService, LikeService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IBestResultService, BestResultService>();
builder.Services.AddTransient<IPartnerService, PartnerService>();
builder.Services.AddSignalR();

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
