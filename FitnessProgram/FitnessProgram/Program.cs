using FitnessProgram.Infrastructure;
using FitnessProgram.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FitnessProgram.Data.Models;
using Microsoft.AspNetCore.Mvc;
using FitnessProgram.Controllers.Hubs;
using static FitnessProgram.Infrastructure.FitnessProgramApiServiceCollectionExtension;

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

builder.Services.AddApplicationServices();

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
