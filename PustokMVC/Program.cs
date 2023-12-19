using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Context;
using PustokMVC.Helpers;
using PustokMVC.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PustokDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSql"));
}).AddIdentity<AppUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<PustokDBContext>();

builder.Services.AddScoped<LayoutService>();

builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Slider}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

PathConstants.RootPath = builder.Environment.WebRootPath;

app.Run();
