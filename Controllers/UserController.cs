using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwiftWheels.Data;
using SwiftWheels.Models;
using System.Linq;

[Authorize(Roles = "ROLE_ADMIN")]
public class UserController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult List()
    {
        var users = _context.Users.ToList();
        return View(users); // Views/User/List.cshtml
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        return View(user); // Views/User/Edit.cshtml
    }

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
        // Şifre ya da rol değiştirme işlemi buraya eklenebilir

        _context.SaveChanges();
        TempData["UserUpdated"] = "Kullanıcı bilgileri başarıyla güncellendi.";
        return RedirectToAction("List");
    }
}
