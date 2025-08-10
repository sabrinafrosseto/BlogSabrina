using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogSabrina.Application.Services;
using BlogSabrina.Application.DTOs;
using BlogSabrina.Domain.Entities;

namespace BlogSabrina.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly BlogService _blogService;

        public AdminController(BlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _blogService.GetAllPostsAsync();
            return View(posts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDto dto)
        {
            if (ModelState.IsValid)
            {
                await _blogService.CreatePostAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var post = await _blogService.GetPostByIdAsync(id);
            if (post == null) return NotFound();

            var dto = new UpdatePostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Summary = post.Summary,
                Category = post.Category,
                Tags = post.Tags,
                IsPublished = post.IsPublished
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePostDto dto)
        {
            if (ModelState.IsValid)
            {
                await _blogService.UpdatePostAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.DeletePostAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}