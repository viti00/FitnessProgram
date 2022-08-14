using FitnessProgram.Infrastructure;
using FitnessProgram.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FitnessProgram.Services.PostServices;
using FitnessProgram.Data.Models;
using Microsoft.AspNetCore.Mvc;
using FitnessProgram.Services.LikeService;
using FitnessProgram.Services.CommentService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.PrepareDatabase();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
