using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Domain.Entities.Identity;
using YGKAPI.Persistence.Contexts.MySQL;

namespace YGKAPI.Persistence.Seed
{
    public static class Seeder
    {
        public static async Task SeedDatabase(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
            var rolesManager = scope.ServiceProvider.GetService<RoleManager<AppRole>>();
            var context = scope.ServiceProvider.GetService<YGKAPIMySQLDbContext>();


            int roleCount = rolesManager.Roles.Count();
            if (roleCount < 1)
                await rolesManager.CreateAsync(new() { Name = "Super Admin", Id = Guid.NewGuid().ToString()});

            int userCount = userManager.Users.Count();
            if (userCount < 1)
            {
                AppUser user = new AppUser()
                {
                    UserName = "admin",
                    Email = "admin@hotmail.com",
                    Name = "admin",
                    Surname = "admin",
                    Id = Guid.NewGuid().ToString(),
                    EmailConfirmed = true
                };
                IdentityResult result = await userManager.CreateAsync(user, "Deneme123..");
                await userManager.AddToRoleAsync(user, "Super Admin");
                await userManager.UpdateAsync(user);
            }

            context.SaveChanges();
        }
    }
}