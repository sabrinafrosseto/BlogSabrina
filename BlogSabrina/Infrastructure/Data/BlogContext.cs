using BlogSabrina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BlogSabrina.Infrastructure.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<BlogPost> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.Summary).HasMaxLength(500);
                entity.Property(e => e.Slug).IsRequired().HasMaxLength(200);
                entity.HasIndex(e => e.Slug).IsUnique();
                
                entity.Property(e => e.Tags)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                        v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>());
            });

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.HasIndex(e => e.Username).IsUnique();
            });

            // Seed data
            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost
                {
                    Id = 1,
                    Title = "Bem-vindos ao meu refúgio digital",
                    Summary = "Um espaço para reflexões sobre a alma humana, crescimento e conexões.",
                    Content = "<p>Como as folhas que dançam ao vento, nossos pensamentos e experiências se entrelaçam formando a tapeçaria da vida. Este é meu espaço sagrado para compartilhar reflexões sobre a jornada humana.</p><p>Aqui, exploraremos os mistérios da mente, os caminhos do coração e as trilhas do crescimento profissional, sempre com a sabedoria ancestral que nos conecta à natureza e uns aos outros.</p>",
                    Author = "Sabrina",
                    CreatedAt = DateTime.Now.AddDays(-1),
                    Category = Category.CienciasSociais,
                    Slug = "bem-vindos-ao-meu-refugio-digital",
                    Tags = new List<string> { "boas-vindas", "reflexão", "jornada" },
                    IsPublished = true
                }
            );

            // Default user (senha: blog2025)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "sabrina",
                    PasswordHash = "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", // blog2025 hasheado
                    CreatedAt = DateTime.Now
                }
            );
        }
    }
}