using BlogSabrina.Application.DTOs;
using BlogSabrina.Domain.Entities;
using BlogSabrina.Domain.Interfaces;

namespace BlogSabrina.Application.Services
{
    public class BlogService
    {
        private readonly IBlogRepository _repository;

        public BlogService(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BlogPost>> GetAllPostsAsync()
            => await _repository.GetAllAsync();

        public async Task<IEnumerable<BlogPost>> GetPostsByCategoryAsync(Category category)
            => await _repository.GetByCategoryAsync(category);

        public async Task<BlogPost?> GetPostBySlugAsync(string slug)
            => await _repository.GetBySlugAsync(slug);

        public async Task<BlogPost?> GetPostByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task<BlogPost> CreatePostAsync(CreatePostDto dto)
        {
            var post = new BlogPost
            {
                Title = dto.Title,
                Content = dto.Content,
                Summary = dto.Summary,
                Category = dto.Category,
                Tags = dto.Tags,
                IsPublished = dto.IsPublished,
                Slug = GenerateSlug(dto.Title)
            };

            return await _repository.CreateAsync(post);
        }

        public async Task<BlogPost> UpdatePostAsync(UpdatePostDto dto)
        {
            var post = await _repository.GetByIdAsync(dto.Id);
            if (post == null) throw new ArgumentException("Post não encontrado");

            post.Title = dto.Title;
            post.Content = dto.Content;
            post.Summary = dto.Summary;
            post.Category = dto.Category;
            post.Tags = dto.Tags;
            post.IsPublished = dto.IsPublished;
            post.UpdatedAt = DateTime.Now;
            post.Slug = GenerateSlug(dto.Title);

            return await _repository.UpdateAsync(post);
        }

        public async Task DeletePostAsync(int id)
            => await _repository.DeleteAsync(id);

        private static string GenerateSlug(string title)
            => title.ToLowerInvariant()
                   .Replace(" ", "-")
                   .Replace("ã", "a").Replace("á", "a").Replace("à", "a")
                   .Replace("é", "e").Replace("ê", "e")
                   .Replace("í", "i").Replace("ó", "o").Replace("ô", "o")
                   .Replace("ú", "u").Replace("ç", "c");
    }
}