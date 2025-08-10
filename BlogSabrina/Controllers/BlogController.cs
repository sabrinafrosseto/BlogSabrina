using Microsoft.AspNetCore.Mvc;
using BlogSabrina.Application.Services;
using BlogSabrina.Domain.Entities;

namespace BlogSabrina.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _blogService.GetAllPostsAsync();
            return View(posts);
        }

        public async Task<IActionResult> Category(string category)
        {
            if (Enum.TryParse<BlogSabrina.Domain.Entities.Category>(category.Replace(" ", ""), out var categoryEnum))
            {
                var posts = await _blogService.GetPostsByCategoryAsync(categoryEnum);
                ViewBag.CategoryName = GetCategoryDisplayName(categoryEnum);
                return View("Index", posts);
            }
            return NotFound();
        }

        public async Task<IActionResult> Post(string slug)
        {
            var post = await _blogService.GetPostBySlugAsync(slug);
            if (post == null) return NotFound();
            return View(post);
        }

        private static string GetCategoryDisplayName(BlogSabrina.Domain.Entities.Category category) => category switch
        {
            BlogSabrina.Domain.Entities.Category.CienciasSociais => "Ciências Sociais",
            BlogSabrina.Domain.Entities.Category.SaudeMental => "Saúde Mental",
            BlogSabrina.Domain.Entities.Category.Carreira => "Carreira",
            _ => category.ToString()
        };
    }
}