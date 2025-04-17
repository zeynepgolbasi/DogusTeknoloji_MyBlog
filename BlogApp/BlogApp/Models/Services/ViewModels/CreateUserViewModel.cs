using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models.Services.ViewModels;

public class CreateUserViewModel
{
    [Required(ErrorMessage = "Kullanıcı ismi boş olamaz")]
    [Display(Name = "User Name :")]
    public string UserName { get; set; } = null!;
    [Required(ErrorMessage = "Email boş olamaz")]
    [Display(Name = "Email :")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "FullName boş olamaz")]
    [Display(Name = "FullName :")]
    public string FullName { get; set; } = null!;
    
    [Required]
    public string Password { get; set; } = null!;
}
