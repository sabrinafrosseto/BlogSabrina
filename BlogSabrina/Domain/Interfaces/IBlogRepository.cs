using BlogSabrina.Domain.Entities;

namespace BlogSabrina.Domain.Interfaces
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<IEnumerable<BlogPost>> GetByCategoryAsync(Category category);
        Task<BlogPost?> GetBySlugAsync(string slug);
        Task<BlogPost?> GetByIdAsync(int id);
        Task<BlogPost> CreateAsync(BlogPost post);
        Task<BlogPost> UpdateAsync(BlogPost post);
        Task DeleteAsync(int id);
    }
}