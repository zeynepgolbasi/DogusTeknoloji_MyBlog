namespace BlogApp.Models.Repositories
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Blog>? Blogs { get; set; }
    }
}
