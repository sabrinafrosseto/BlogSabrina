using BlogSabrina.Application.Services;
using BlogSabrina.Domain.Interfaces;
using BlogSabrina.Infrastructure.Data;
using BlogSabrina.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
    });

// Database
builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlite("Data Source=blog.db"));

// DI
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<BlogService>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// Database migration
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BlogContext>();
    context.Database.EnsureCreated();
    
    // Criar usuário padrão se não existir
    if (!context.Users.Any())
    {
        var authService = scope.ServiceProvider.GetRequiredService<BlogSabrina.Application.Services.AuthService>();
        var defaultUser = new BlogSabrina.Domain.Entities.User
        {
            Username = "sabrina",
            PasswordHash = HashPassword("blog2025"),
            CreatedAt = DateTime.Now
        };
        context.Users.Add(defaultUser);
        context.SaveChanges();
    }
}

static string HashPassword(string password)
{
    using var sha256 = System.Security.Cryptography.SHA256.Create();
    var salt = "BlogSabrina2025";
    var saltedPassword = password + salt;
    var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(saltedPassword));
    return Convert.ToBase64String(hashedBytes);
}

// Configure for production
app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "blog",
    pattern: "Blog/Category/{category}",
    defaults: new { controller = "Blog", action = "Category" });

app.MapControllerRoute(
    name: "post",
    pattern: "Blog/Post/{slug}",
    defaults: new { controller = "Blog", action = "Post" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
