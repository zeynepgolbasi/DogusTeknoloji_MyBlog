using BlogApp.Models.Repositories.Entities;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models.Repositories
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime PublishDate { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public string UserId { get; set; } = null!; // Identity'de string türündedir
        public ApplicationUser User { get; set; } = null!;
    }
}
