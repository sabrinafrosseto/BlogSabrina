namespace BlogSabrina.Domain.Entities
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Author { get; set; } = "Sabrina";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public Category Category { get; set; }
        public string Slug { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new();
        public bool IsPublished { get; set; } = true;
    }

    public enum Category
    {
        CienciasSociais = 1,
        SaudeMental = 2,
        Carreira = 3
    }
}