using BlogSabrina.Domain.Entities;
using BlogSabrina.Domain.Interfaces;
using BlogSabrina.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogSabrina.Infrastructure.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
            => await _context.Posts
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

        public async Task<IEnumerable<BlogPost>> GetByCategoryAsync(Category category)
            => await _context.Posts
                .Where(p => p.IsPublished && p.Category == category)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

        public async Task<BlogPost?> GetBySlugAsync(string slug)
            => await _context.Posts
                .FirstOrDefaultAsync(p => p.Slug == slug && p.IsPublished);

        public async Task<BlogPost?> GetByIdAsync(int id)
            => await _context.Posts.FindAsync(id);

        public async Task<BlogPost> CreateAsync(BlogPost post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<BlogPost> UpdateAsync(BlogPost post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
    }
}