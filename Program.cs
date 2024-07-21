using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCIDentity.Core.IServices;
using MVCIDentity.Data;
using MVCIDentity.Helper;
using MVCIDentity.Models;
using MVCIDentity.Service;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddRazorPages();

        builder.Services.AddIdentity<Identity, IdentityRole>(option =>
        {
            option.Password.RequireNonAlphanumeric = false;
            option.Password.RequiredLength = 3;
            option.Password.RequireLowercase = false;
            option.Password.RequireUppercase = false;

        }).AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminRolePolicy", policy =>
            policy.RequireRole("Admin")
            ); 
            
            options.AddPolicy("UserRolePolicy", policy =>
            policy.RequireRole("User")
            );

            options.AddPolicy("UserAdminRolePolicy", policy =>
           policy.RequireRole("User").RequireRole("Admin")
           );

        });



        builder.Services.AddScoped<IUserClaimsPrincipalFactory<Identity>, CustomeUserClaimsPrincipalFactory>();
        builder.Services.AddScoped<ICarService, CarService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");


        app.Run();
    }


}