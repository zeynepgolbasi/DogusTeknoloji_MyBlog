using BlogApp.Models.Services;
using BlogApp.Models.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        // Constructor
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Kategorileri listeleme
       
        public IActionResult Index()
        {
            IEnumerable<CategoryViewModel> categories = _categoryService.GetAll();
            return View(categories);
        }

        // Kategori oluşturma için GET metod
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var viewModel = _categoryService.CreateViewModel();
            return View(viewModel);
        }

        // Kategori oluşturma için POST metod
        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Eğer ModelState geçersizse, CreateViewModel ile formu tekrar göster
                return View(_categoryService.CreateViewModel());
            }

            // Kategori oluşturma
            _categoryService.Create(viewModel);
            return RedirectToAction("Index");
        }

        // Kategori silme işlemi
        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            _categoryService.Remove(id);
            return RedirectToAction("Index");
        }

        // Kategori düzenleme için GET metod
        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            var viewModel = _categoryService.EditViewModel(id);

            if (viewModel == null)
            {
                return NotFound();  // Eğer kategori bulunamazsa 404 döner
            }

            return View(viewModel);
        }

        // Kategori düzenleme için POST metod
        [HttpPost]
        [Authorize]
        public IActionResult Edit(EditCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Eğer ModelState geçersizse, EditViewModel ile formu tekrar göster
                return View(_categoryService.EditViewModel(viewModel.Id));
            }

            // Kategoriyi güncelle
            _categoryService.Update(viewModel);
            return RedirectToAction("Index");
        }
    }
}


