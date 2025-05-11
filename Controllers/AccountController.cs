using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SwiftWheels.Data;
using SwiftWheels.Models;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System;

namespace SwiftWheels.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // SHA256 hash fonksiyonu (tüm işlemlerde kullanılacak)
        private string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                ViewBag.Error = "Geçersiz kullanıcı.";
                return View();
            }

            var hashed = HashPassword(password);
            if (user.Password != hashed)
            {
                ViewBag.Error = "Geçersiz e-posta veya şifre.";
                return View();
            }

            // Giriş başarılı → cookie oturum oluştur
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.GivenName, user.Name + " " + user.Surname),
                new Claim(ClaimTypes.Role, user.Roles)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            TempData["LoginSuccess"] = $"Hoş geldin, {user.Name} {user.Surname}!";

            return user.Roles == "ROLE_ADMIN"
                ? RedirectToAction("Index", "AdminRental")
                : RedirectToAction("Index", "Vehicle");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User newUser)
        {
            if (!ModelState.IsValid)
                return View(newUser);

            if (_context.Users.Any(u => u.Email == newUser.Email))
            {
                ViewBag.Error = "Bu e-mail zaten kayıtlı.";
                return View(newUser);
            }

            // SHA256 ile hashle
            newUser.Password = HashPassword(newUser.Password);

            newUser.Roles = newUser.Email.ToLower() == "admin@swiftwheels.com" ? "ROLE_ADMIN" : "ROLE_USER";
            newUser.SystemUser = newUser.Roles == "ROLE_ADMIN";

            _context.Users.Add(newUser);
            _context.SaveChanges();

            ViewBag.RegisterSuccess = true;
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
