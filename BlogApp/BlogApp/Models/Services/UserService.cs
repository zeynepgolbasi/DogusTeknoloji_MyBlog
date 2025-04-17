using BlogApp.Models.Repositories.Entities;
using BlogApp.Models.Services.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Models.Services
{
    public class UserService : IUserService
    {
        private readonly RoleManager<AppRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<AppRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        public bool CreateUser(CreateUserViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PasswordHash = model.Password,
                FullName = model.FullName,
            };
            var result = userManager.CreateAsync(user, model.Password).Result;


            return result.Succeeded;
        }

        public bool SignIn(SignInViewModel model)
        {
            var hasUser = userManager.FindByEmailAsync(model.Email).Result;

            if (hasUser == null) return false;


            var result = signInManager.PasswordSignInAsync(hasUser.UserName!, model.Password, true, false).Result;
            return result.Succeeded;
        }

        public void SignOut()
        {
            signInManager.SignOutAsync();
        }
    }
}
