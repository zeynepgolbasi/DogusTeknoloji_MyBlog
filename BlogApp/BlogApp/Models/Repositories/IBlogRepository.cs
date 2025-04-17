namespace BlogApp.Models.Repositories
{
    public interface IBlogRepository
    {
        List<Blog> GetAll();
       
        Blog? GetById(int id);
        void Add(Blog blog);
        void Update(Blog blog);
        void Delete(Blog blog);
    }
}
