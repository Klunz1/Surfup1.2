using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurfsupEmil.Models;
using System.Diagnostics;

namespace SurfsupEmil.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceProvider _serviceProvider;

        public HomeController(ILogger<HomeController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            CreateAdminRoleAndUser(_serviceProvider).Wait();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private async Task CreateAdminRoleAndUser(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "Admin" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    var roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                    if (!roleResult.Succeeded)
                    {
                        Console.WriteLine($"Error creating role {roleName}: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                    }
                }
            }

            var user = await UserManager.FindByEmailAsync("admin@admin.com");

            if (user == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                    NormalizedUserName = "ADMIN",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var createPowerUser = await UserManager.CreateAsync(adminUser, "Password123!");
                if (createPowerUser.Succeeded)
                {
                    var addToRoleResult = await UserManager.AddToRoleAsync(adminUser, "Admin");
                    if (!addToRoleResult.Succeeded)
                    {
                        Console.WriteLine($"Error adding user to Admin role: {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    Console.WriteLine($"Error creating user: {string.Join(", ", createPowerUser.Errors.Select(e => e.Description))}");
                }
            }
            if (user != null)
            {
                // Reset the password
                var removePasswordResult = await UserManager.RemovePasswordAsync(user);
                if (removePasswordResult.Succeeded)
                {
                    var addPasswordResult = await UserManager.AddPasswordAsync(user, "NewPassword123!");
                    if (addPasswordResult.Succeeded)
                    {
                        Console.WriteLine("Password reset successfully!");
                    }
                    else
                    {
                        Console.WriteLine($"Error setting new password: {string.Join(", ", addPasswordResult.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    Console.WriteLine($"Error removing password: {string.Join(", ", removePasswordResult.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                Console.WriteLine("Admin user not found.");
            }
        }
    }

    //private async Task CreateAdminRoleAndUser(IServiceProvider serviceProvider)
    //{
    //    var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    //    var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

    //    //initializing custom roles 
    //    string[] roleNames = { "Admin" };

    //    IdentityResult roleResult;
    //    foreach (var roleName in roleNames)
    //    {
    //        var roleExist = await RoleManager.RoleExistsAsync(roleName);
    //        if (!roleExist)
    //        {
    //            //create the roles and seed them to the database: Question 1
    //            roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
    //        }
    //    }

    //    //Initializing custom users.
    //    var adminUser = new IdentityUser
    //    {
    //        UserName = "admin",
    //        Email = "admin@admin.com",
    //        EmailConfirmed = true,
    //        NormalizedUserName = Guid.NewGuid().ToString(),
    //        SecurityStamp = Guid.NewGuid().ToString()
    //    };

    //    var user = await UserManager.FindByEmailAsync(adminUser.Email);

    //    if (user == null)
    //    {
    //        var CreatePowerUser = await UserManager.CreateAsync(adminUser, "Password123!");
    //        if (CreatePowerUser.Succeeded)
    //        {
    //            await UserManager.AddToRoleAsync(adminUser, "Admin");
    //        }
    //    }
    //}
}