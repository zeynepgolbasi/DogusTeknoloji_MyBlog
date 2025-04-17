using BlogApp.Models.Services.ViewModels;

namespace BlogApp.Models.Services
{
    public interface IUserService
    {
        bool CreateUser(CreateUserViewModel model);
        bool SignIn(SignInViewModel model);
        void SignOut();
    }
}
