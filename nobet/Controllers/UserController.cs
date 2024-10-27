using emergencyProject.DataDb;
using emergencyProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using nobet.Models;
using BCrypt.Net;



//0 admin 1professor 2asistan  . Role kullanımında hatırlatma 
namespace emergencyProject.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context) // Yapıcı metot düzeltildi
        {
            _context = context;
        }
  

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user != null && VerifyPassword(model.Password, user.Password)) // Düz metin doğrulama
            {
                // Kullanıcı bilgilerini oturumda sakla
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()) // Rol bilgisi
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe // Eğer "Beni Hatırla" kutucuğu işaretlendiyse
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                // Kullanıcıyı yönlendirme
                if (user.Role == UserRole.Admin)
                {
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else if (user.Role == UserRole.Professor)
                {
                    return RedirectToAction("ProfessorDashboard", "Professor");
                }
                else if (user.Role == UserRole.Assistant)
                {
                    return RedirectToAction("AssistantDashboard", "Assistant");
                }
                else { 

                return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
            return View(model);
        }
        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            return enteredPassword == storedPassword; // Düz metin karşılaştırması
        }
        [HttpPost] //BURAYA BAKKKK
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home"); // Giriş sayfasına yönlendir
        }
    }
}
