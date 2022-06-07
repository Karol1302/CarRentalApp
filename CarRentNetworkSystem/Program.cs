using ATHCarRentNetworkSystem.Data;
using ATHCarRentNetworkSystem.Mapper;
using ATHCarRentNetworkSystem.Models;
using ATHCarRentNetworkSystem.Repositories;
using ATHCarRentNetworkSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));//builder.Services.AddDbContext<ApplicationDbContext>(options =>
////    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("AthCars"));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()   //dodane
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));
builder.Services.AddScoped(typeof(IStringRepositoryService<>), typeof(StringRepositoryService<>));
builder.Services.AddTransient<IAuthorizationInitializer, AuthorizationInitializer>();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CarRentPlance}/{action=Index}/{id?}"
    );*/
using (var scope= app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetService<IAuthorizationInitializer>();
    initializer.GenerateAdminAndRoles();
}

app.Run();
