using BlogSabrina.Domain.Entities;

namespace BlogSabrina.Application.DTOs
{
    public class CreatePostDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public Category Category { get; set; }
        public List<string> Tags { get; set; } = new();
        public bool IsPublished { get; set; } = true;
    }

    public class UpdatePostDto : CreatePostDto
    {
        public int Id { get; set; }
    }
}