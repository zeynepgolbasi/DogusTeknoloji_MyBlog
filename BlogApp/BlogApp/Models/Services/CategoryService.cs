using BlogApp.Models.Repositories;
using BlogApp.Models.Services.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.Models.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBlogRepository _blogRepository;

        public CategoryService(ICategoryRepository categoryRepository, IBlogRepository blogRepository)
        {
            _categoryRepository = categoryRepository;
            _blogRepository = blogRepository;
        }

        // Kategori oluşturma
        public void Create(CreateCategoryViewModel createCategoryViewModel)
        {
            var category = new Category
            {
                Name = createCategoryViewModel.Name
            };

            _categoryRepository.Add(category);
        }

        // Kategori oluşturma için ViewModel
        public CreateCategoryViewModel CreateViewModel()
        {
            return new CreateCategoryViewModel();
        }

        // Edit işlemi için ViewModel
        public EditCategoryViewModel? EditViewModel(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null) return null;

            return new EditCategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        // Kategori güncelleme işlemi
        public void Update(EditCategoryViewModel editCategoryViewModel)
        {
            var category = _categoryRepository.GetById(editCategoryViewModel.Id);
            if (category != null)
            {
                category.Name = editCategoryViewModel.Name;
                _categoryRepository.Update(category);
            }
        }

        // Kategorileri listeleme
        public List<CategoryViewModel> GetAll()
        {
            var categoryList = _categoryRepository.GetAll();
            var categoryViewModelList = new List<CategoryViewModel>();

            foreach (var category in categoryList)
            {
                var categoryViewModel = new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name
                };

                categoryViewModelList.Add(categoryViewModel);
            }

            return categoryViewModelList;
        }

        // Kategoriye göre ID ile kategori getirme
        public CategoryViewModel? GetById(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null) return null;

            return new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        // Kategori silme
        public void Remove(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category != null)
            {
                _categoryRepository.Delete(category);
            }
        }
    }

}
