using Microsoft.AspNetCore.Identity;
using Swaed.Helpers;
using Swaed.Models;

namespace Swaed.Data.Seeds
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = UserRoles.GetRoles();

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminEmail = "admin@example.com";
            var adminPassword = "Admin@123";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new Admin { UserName = adminEmail, Email = adminEmail };
                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }

}
