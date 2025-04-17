using Microsoft.AspNetCore.Identity;

namespace BlogApp.Models.Repositories.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
        public string FullName { get; set; } = null!;
    }
}
