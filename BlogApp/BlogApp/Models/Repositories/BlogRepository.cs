using Microsoft.EntityFrameworkCore;

namespace BlogApp.Models.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _context;

        public BlogRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Blog> GetAll()
        {
            return _context.Blogs.Include(x => x.Category).ToList();
        }

        public Blog? GetById(int id)
        {
            return _context.Blogs.Find(id);
        }

        public void Add(Blog blog)
        {
            _context.Blogs.Add(blog);
            _context.SaveChanges();
        }

        public void Update(Blog blog)
        {
            _context.Blogs.Update(blog);
            _context.SaveChanges();
        }

        public void Delete(Blog blog)
        {
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
        }
    }
}
