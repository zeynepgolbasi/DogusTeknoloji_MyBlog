using BlogApp.Models.Repositories;

namespace BlogApp.Models.Services.ViewModels
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime PublishDate { get; set; }
        public string? ImageUrl { get; set; }
        public string CategoryName { get; set; } = null!;
        public int CategoryId { get; set; } 
    }
}
