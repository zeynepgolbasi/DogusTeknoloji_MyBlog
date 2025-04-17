using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using BlogApp.Models.Services;
using BlogApp.Models.Repositories;
using BlogApp.Models.Services.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBlogService _blogService;


    public HomeController(ILogger<HomeController> logger, IBlogService blogService)
    {
        _logger = logger;
        _blogService = blogService;
    }

    public IActionResult Index(int? categoryId)
    {
        var categories = _blogService.GetCategories();
        ViewBag.SelectedCategoryId = categoryId;
        ViewBag.Categories = new SelectList(_blogService.GetCategories(), "Id", "Name");

        IEnumerable<BlogViewModel> blogs;
        if (categoryId != null)
        {
            blogs = _blogService.GetAllById(categoryId);
        }
        else

            blogs = _blogService.GetAll();

        return View(blogs);
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
}
