using BlogApp.Models.Repositories;
using BlogApp.Models.Services.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.Models.Services
{
    public class BlogService : IBlogService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository, ICategoryRepository categoryRepository)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
        }

        public List<BlogViewModel> GetAllById(int? categoryId)
        {
            var blogList = categoryId == 0
          ? _blogRepository.GetAll().ToList()  // Kategori ID'si 0 ise, tüm blogları getir
          : _blogRepository.GetAll().Where(b => b.CategoryId == categoryId).ToList();  // Belirli kategoriye ait blogları getir


            var blogViewModelList = new List<BlogViewModel>();

            foreach (var blog in blogList)
            {
                var blogViewModel = new BlogViewModel
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Content = blog.Content,
                    PublishDate = blog.PublishDate,
                    CategoryName = blog.Category.Name,
                    CategoryId = blog.CategoryId

                };

                blogViewModelList.Add(blogViewModel);
            }

            return blogViewModelList;

        }
        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepository.GetAll().Distinct().ToList();
        }
        public CreateBlogViewModel CreateViewModel()
        {
            var categories = _categoryRepository.GetAll();

            var createBlogViewModel = new CreateBlogViewModel
            {
                PublishDate = DateTime.Now
            };

            createBlogViewModel.CategoryList = new SelectList(categories, "Id", "Name");

            return createBlogViewModel;
        }
        public CreateBlogViewModel CreateViewModel(CreateBlogViewModel createBlogViewModel)
        {
            var categories = _categoryRepository.GetAll();

            createBlogViewModel.CategoryList = new SelectList(categories, "Id", "Name", createBlogViewModel.CategoryId);

            return createBlogViewModel;
        }


        public void Create(CreateBlogViewModel createBlogViewModel)
        {
            var blog = new Blog
            {
                Title = createBlogViewModel.Title!,
                Content = createBlogViewModel.Content!,
                CategoryId = createBlogViewModel.CategoryId!.Value,
                PublishDate = DateTime.Now,
                ImageUrl = createBlogViewModel.ImageUrl!,
            };
            _blogRepository.Add(blog);
        }


        public BlogViewModel? GetById(int id)
        {
            var blog = _blogRepository.GetById(id);

            if (blog == null) return null!;
            var BlogViewModel = new BlogViewModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                PublishDate = blog.PublishDate,
                ImageUrl = blog.ImageUrl,
                CategoryName = blog.Category.Name
            };
            return BlogViewModel;
        }

        public void Remove(int id)
        {
            var blog = _blogRepository.GetById(id);
            if (blog != null) _blogRepository.Delete(blog);
        }

        public EditBlogViewModel? EditViewModel(int id)
        {
            var blog = _blogRepository.GetById(id);


            if (blog == null) return null;
            var categories = _categoryRepository.GetAll();
            var editBlogViewModel = new EditBlogViewModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                CategoryId = blog.CategoryId,
                PublishDate = blog.PublishDate
            };
            editBlogViewModel.CategoryList = new SelectList(categories, "Id", "Name", editBlogViewModel.CategoryId);
            return editBlogViewModel;
        }


        public void Update(EditBlogViewModel editBlogViewModel)
        {
            var blog = _blogRepository.GetById(editBlogViewModel.Id);
            if (blog == null) return;

            blog.Title = editBlogViewModel.Title!;
            blog.Content = editBlogViewModel.Content!;
            blog.CategoryId = editBlogViewModel.CategoryId!.Value;
            blog.PublishDate = editBlogViewModel.PublishDate;
            _blogRepository.Update(blog);
        }

        public EditBlogViewModel? EditViewModel(EditBlogViewModel editBlogViewModel)
        {
            var categories = _categoryRepository.GetAll();
            editBlogViewModel.CategoryList = new SelectList(categories, "Id", "Name", editBlogViewModel.CategoryId);
            return editBlogViewModel;
        }

        public List<BlogViewModel> GetAll()
        {
            var blogList = _blogRepository.GetAll().ToList();


            var blogViewModelList = new List<BlogViewModel>();

            foreach (var blog in blogList)
            {
                var blogViewModel = new BlogViewModel
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Content = blog.Content,
                    PublishDate = blog.PublishDate,
                    CategoryName = blog.Category.Name
                };

                blogViewModelList.Add(blogViewModel);
            }

            return blogViewModelList;
        }
    }
}
