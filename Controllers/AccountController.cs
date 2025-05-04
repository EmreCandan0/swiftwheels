using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SwiftWheels.Data;
using SwiftWheels.Models;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SwiftWheels.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                ViewBag.Error = "Invalid credentials.";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.GivenName, user.Name + " " + user.Surname),
                new Claim(ClaimTypes.Role, user.Roles)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // ✅ Login sonrası hoş geldin mesajı
            TempData["LoginSuccess"] = $"Welcome back, {user.Name} {user.Surname}!";

            if (user.Roles == "ROLE_ADMIN")
                return RedirectToAction("Index", "AdminRental");
            else
                return RedirectToAction("Index", "Vehicle");
        }

        public IActionResult AccessDenied()
        {
            return View();
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
            {
                return View(newUser);
            }

            if (_context.Users.Any(u => u.Email == newUser.Email))
            {
                ViewBag.Error = "Bu e-mail zaten kayıtlı.";
                return View(newUser);
            }

            newUser.Roles = "ROLE_USER";
            newUser.SystemUser = false;

            _context.Users.Add(newUser);
            _context.SaveChanges();

            ViewBag.RegisterSuccess = true;
            return View(); // Login'e yönlendirmiyoruz!
        }

    }
}
