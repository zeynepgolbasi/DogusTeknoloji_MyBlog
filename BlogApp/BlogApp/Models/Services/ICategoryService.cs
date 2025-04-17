using BlogApp.Models.Services.ViewModels;

namespace BlogApp.Models.Services
{
    public interface ICategoryService
    {
        // Kategorileri listele
        List<CategoryViewModel> GetAll();

        // Kategori oluşturmak için ViewModel döndür
        CreateCategoryViewModel CreateViewModel();

        // Kategori oluşturma işlemi
        void Create(CreateCategoryViewModel createCategoryViewModel);

        // Kategoriyi id'ye göre getir
        CategoryViewModel? GetById(int id);

        // Kategori düzenlemek için ViewModel döndür
        EditCategoryViewModel? EditViewModel(int id);

        // Kategori düzenleme işlemi
        void Update(EditCategoryViewModel editCategoryViewModel);

        // Kategori silme işlemi
        void Remove(int id);
    }

}
