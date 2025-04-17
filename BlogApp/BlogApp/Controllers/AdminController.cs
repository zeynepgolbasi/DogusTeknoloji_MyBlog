using BlogApp.Models.Repositories.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class AdminController(UserManager<ApplicationUser> userManager, RoleManager<AppRole> roleManager) : Controller
    {
        public IActionResult Index()
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var roleToCreateResult = roleManager.CreateAsync(new AppRole { Name = "Admin" }).Result;

                if (roleToCreateResult.Succeeded)
                {
                    var hasUser = userManager.FindByEmailAsync("zeynepgolbasi2000@gmail.com").Result;

                    if (hasUser is not null)
                    {
                        userManager.AddToRoleAsync(hasUser, "Admin").Wait();
                    }
                }
            }

            return View();
        }
    }
}
