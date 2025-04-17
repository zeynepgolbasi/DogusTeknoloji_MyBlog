using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models.Services.ViewModels
{
    public class EditBlogViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Blog başlığı boş olamaz")]
        [Display(Name = "Blog Title :")]
        public string? Title { get; set; } = null!;


        [Required(ErrorMessage = "Blog içeriği boş olamaz")]
        [Display(Name = "Content :")]
        public string? Content { get; set; }

        [Required(ErrorMessage = "Kategori seçiniz")]
        [Display(Name = "Category :")]
        public int? CategoryId { get; set; }
        [ValidateNever] public SelectList CategoryList { get; set; } = null!;
        public DateTime PublishDate { get; set; }
    }
}
