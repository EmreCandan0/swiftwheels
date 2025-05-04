using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftWheels.Data;
using System.Linq;

[Authorize(Roles = "ROLE_ADMIN")]
public class AdminRentalController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminRentalController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var rentals = _context.RentedVehicles
            .Include(r => r.Vehicle)
            .Include(r => r.User)
            .ToList();

        return View(rentals);
    }
    public IActionResult AllRented()
    {
        var rentals = _context.RentedVehicles
            .Include(r => r.Vehicle)
            .Include(r => r.User)
            .ToList();

        return View(rentals); // Views/AdminRental/AllRented.cshtml
    }
    [HttpPost]
    [Authorize(Roles = "ROLE_ADMIN")]
    public IActionResult Delete(int id)
    {
        var rental = _context.RentedVehicles.FirstOrDefault(r => r.Id == id);
        if (rental == null)
        {
            return NotFound();
        }

        _context.RentedVehicles.Remove(rental);
        _context.SaveChanges();

        return RedirectToAction("AllRented");
    }


}
