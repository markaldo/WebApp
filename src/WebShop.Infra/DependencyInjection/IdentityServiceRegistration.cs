using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Infra.Identity;
using WebShop.Infra.Persistence;

namespace WebShop.Infra.DependencyInjection
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection AddIdentityServices(
            this IServiceCollection services,
            string connectionString)
        {
            //EF Core
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

            // Identity with roles and tokens (MVC-ready)
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // Cookie path
            services.ConfigureApplicationCookie(cookie =>
            {
                cookie.LoginPath = "/Account/Login";
                cookie.LogoutPath = "/Account/Logout";
                cookie.AccessDeniedPath = "/Account/AccessDenied";
                cookie.SlidingExpiration = true;
                cookie.ExpireTimeSpan = TimeSpan.FromHours(8);
            });

            return services;
        }
    }
}
