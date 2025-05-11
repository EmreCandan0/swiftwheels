using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwiftWheels.Data;
using SwiftWheels.Models;
using SwiftWheels.Models.ViewModels;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class UserController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ 1. KENDİ PROFİLİNİ GÖRÜNTÜLE/DUZENLE (Tüm roller erişebilir)
    [Authorize]
    [HttpGet]
    public IActionResult Profile()
    {
        var email = User.Identity.Name;
        var user = _context.Users.FirstOrDefault(u => u.Email == email);

        if (user == null)
            return NotFound();

        var model = new UserProfileViewModel
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email
        };

        return View(model);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Profile(UserProfileViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = _context.Users.FirstOrDefault(u => u.Id == model.Id);
        if (user == null)
            return NotFound();

        user.Name = model.Name;
        user.Surname = model.Surname;
        user.PhoneNumber = model.PhoneNumber;

        _context.SaveChanges();
        ViewBag.Message = "Profil başarıyla güncellendi.";
        return View(model);
    }

    // ✅ 2. TÜM KULLANICILARI GÖR (SADECE ADMIN)
    [Authorize(Roles = "ROLE_ADMIN")]
    public IActionResult List()
    {
        var users = _context.Users.ToList();
        return View(users); // Views/User/List.cshtml
    }

    // ✅ 3. KULLANICIYI DÜZENLE (SADECE ADMIN)
    [Authorize(Roles = "ROLE_ADMIN")]
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        return View(user); // Views/User/Edit.cshtml
    }

    [Authorize(Roles = "ROLE_ADMIN")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(User updatedUser)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id);
        if (user == null)
            return NotFound();

        user.Name = updatedUser.Name;
        user.Surname = updatedUser.Surname;
        user.Email = updatedUser.Email;
        user.PhoneNumber = updatedUser.PhoneNumber;
        // Gerekirse şifre veya rol güncelleme işlemleri buraya eklenebilir

        _context.SaveChanges();
        TempData["UserUpdated"] = "Kullanıcı bilgileri başarıyla güncellendi.";
        return RedirectToAction("List");
    }

    [Authorize]
    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View();
    }



    private string HashPassword(string password)
    {
        using (var sha = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

    [Authorize]
    [HttpPost]
    public IActionResult ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var email = User.Identity.Name;
        var user = _context.Users.FirstOrDefault(u => u.Email == email);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı.");
            return View(model);
        }

        // GİRİLEN mevcut şifreyi hashle
        var hashedCurrentPassword = HashPassword(model.CurrentPassword);

        // Hash karşılaştırması
        if (user.Password != hashedCurrentPassword)
        {
            ModelState.AddModelError("CurrentPassword", "Mevcut şifre hatalı.");
            return View(model);
        }

        // Yeni şifreyi hashle ve kaydet
        user.Password = HashPassword(model.NewPassword);
        _context.SaveChanges();

        ViewBag.Message = "Şifre başarıyla güncellendi.";
        return View();
    }


    [Authorize(Roles = "ROLE_ADMIN")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        _context.Users.Remove(user);
        _context.SaveChanges();

        return Ok(); // AJAX'a success
    }

}
