using BlogApp.Models.Repositories;
using BlogApp.Models.Services.ViewModels;

namespace BlogApp.Models.Services
{
    public interface IBlogService
    {
        //  List<BlogViewModel> GetAll();
        List<BlogViewModel> GetAllById(int? categoryId);
        List<BlogViewModel> GetAll();
        //IEnumerable<string> GetCategories();
        IEnumerable<Category> GetCategories();
        CreateBlogViewModel CreateViewModel();

        CreateBlogViewModel CreateViewModel(CreateBlogViewModel createBlogViewModel);

        void Create(CreateBlogViewModel createBlogViewModel);
        BlogViewModel? GetById(int id);
        EditBlogViewModel? EditViewModel(int id);
        EditBlogViewModel? EditViewModel(EditBlogViewModel editBlogViewModel);
        void Remove(int id);
        void Update(EditBlogViewModel editBlogViewModel);
    }
}
