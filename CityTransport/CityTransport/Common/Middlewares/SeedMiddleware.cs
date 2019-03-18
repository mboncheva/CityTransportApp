namespace CityTransport.Web.Common.Middlewares
{
    using CityTransport.Common.Constants;
    using CityTransport.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class SeedMiddleware
    {
        private static readonly IdentityRole[] roles =
        {
            new IdentityRole(SeedDataConstants.AdminRole),
            new IdentityRole(SeedDataConstants.UserRole),
        };

        private readonly RequestDelegate next;

        public SeedMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context,
            IServiceProvider serviceProvider,
            SignInManager<User> signInManager)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();


            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

            if (!userManager.Users.Any())
            {
                var adminUser = new User
                {
                    UserName = SeedDataConstants.AdminUsername,
                    Email = SeedDataConstants.AdminEmail
                };

                await userManager.CreateAsync(adminUser, SeedDataConstants.AdminPassword);

                await userManager.AddToRoleAsync(adminUser, SeedDataConstants.AdminRole);
            }

            await this.next(context);
        }
    }
}