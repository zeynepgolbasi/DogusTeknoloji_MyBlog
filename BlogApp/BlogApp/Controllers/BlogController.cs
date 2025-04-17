using BlogApp.Models.Repositories;
using BlogApp.Models.Services;
using BlogApp.Models.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;        
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            IEnumerable<BlogViewModel> blogs = _blogService.GetAll();
            return View(blogs); 
        }

        [HttpGet]
        
        public IActionResult Create()
        {
            var viewModel = _blogService.CreateViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateBlogViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(_blogService.CreateViewModel(viewModel));

            _blogService.Create(viewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _blogService.Remove(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _blogService.EditViewModel(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditBlogViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(_blogService.EditViewModel(viewModel));

            _blogService.Update(viewModel);
            return RedirectToAction("Index");
        }
    }
}
