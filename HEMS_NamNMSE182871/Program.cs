using BusinessLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace HEMS_NamNMSE182871;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        // DbContext from web appsettings.json
        builder.Services.AddDbContext<Sp25HospitalEquipmentDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Repositories (concrete)
        builder.Services.AddScoped<AccountRepository>();
        builder.Services.AddScoped<SupplierRepository>();
        builder.Services.AddScoped<EquipmentRepository>();

        // Services (concrete)
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<SupplierService>();
        builder.Services.AddScoped<EquipmentService>();

        // Authentication
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });

        // Authorization Policies
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ManagerOnly", policy => policy.RequireAssertion(context =>
                context.User.HasClaim("Role", "2")));
            
            options.AddPolicy("ManagerOrStaff", policy => policy.RequireAssertion(context =>
                context.User.HasClaim("Role", "2") || context.User.HasClaim("Role", "3")));
        });

        // SignalR
        builder.Services.AddSignalR();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        // Default route to Login
        app.MapGet("/", () => Results.Redirect("/Account/Login"));
        
        app.MapRazorPages();
        
        // SignalR Hub
        app.MapHub<Hubs.EquipmentHub>("/equipmentHub");

        app.Run();
    }
}
