using BlogApp.Models.Services.ViewModels;
using BlogApp.Models.Services;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models.Repositories.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using BlogApp.Models.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Controllers
{
    public class AuthController(UserManager<ApplicationUser> userManager, RoleManager<AppRole> roleManager, SignInManager<ApplicationUser> signInManager, IUserService userService) : Controller
    {
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Şifreyi hash'leme
                string hashedPassword = HashPassword(model.Password);

                // Kullanıcıyı veritabanına ekleyelim
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                    PasswordHash = model.Password
                };

                // Kullanıcıyı veritabanına kaydedelim
                //var result = await userManager.CreateAsync(user, user.PasswordHash);
                //await userManager.AddToRoleAsync(user, "Admin");

                var result = userManager.CreateAsync(user, model.Password).Result;
                var userO = userService.CreateUser(model);

                //context.Add(user);
                //await context.SaveChangesAsync();

                // Başarıyla kaydedildikten sonra ana sayfaya yönlendirelim
                return RedirectToAction("Index", "Blog");
            }

            // Model geçerli değilse, tekrar formu göster
            return View(model);
        }

        // Şifreyi hash'leme fonksiyonu
        private string HashPassword(string password)
        {
            // Salt değerini oluştur
            byte[] salt = new byte[128 / 8];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Şifreyi hash'le
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = HashPassword(model.Password);

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    // Giriş başarılı ise Blog Index sayfasına yönlendir
                    return RedirectToAction("AccessDenied", "Auth");
                }
                else
                {
                    // Eğer giriş başarısızsa hata mesajı ekle
                    ModelState.AddModelError("", "Email veya şifre yanlış");
                }
            }
            return View(model); // Model geçerli değilse formu tekrar göster
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();  // Çıkış yap
            return RedirectToAction("Index", "Home");  // Anasayfaya yönlendir
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
