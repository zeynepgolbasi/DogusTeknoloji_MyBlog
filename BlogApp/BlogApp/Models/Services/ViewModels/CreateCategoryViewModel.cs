using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models.Services.ViewModels
{
    public class CreateCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategory ismi boş olamaz")]
        [Display(Name = "Category Name :")]
        public string Name { get; set; } = null!;
    }
}
