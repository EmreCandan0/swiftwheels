using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftWheels.Data;
using SwiftWheels.Models;
using System;
using System.Linq;

[Authorize(Roles = "ROLE_USER")]
public class RentedVehicleController : Controller
{
    private readonly ApplicationDbContext _context;

    public RentedVehicleController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var email = User.Identity.Name;
        var user = _context.Users.FirstOrDefault(u => u.Email == email);

        var rentals = _context.RentedVehicles
            .Where(rv => rv.UserId == user.Id)
            .Include(rv => rv.Vehicle) // Burayı ekle
            .ToList();

        return View(rentals);
    }

    [HttpGet]
public IActionResult Create(int id)
{
    var vehicle = _context.Vehicles.FirstOrDefault(v => v.Id == id);
    if (vehicle == null)
        return NotFound();

    var rentedDates = _context.RentedVehicles
        .Where(rv => rv.VehicleId == id)
        .ToList()
        .SelectMany(rv => Enumerable.Range(0, (rv.EndDate - rv.StartDate).Days)
            .Select(offset => rv.StartDate.AddDays(offset).ToString("yyyy-MM-dd")))
        .Distinct()
        .ToList();

    ViewBag.VehiclePrice = vehicle.Price;
    ViewBag.DisabledDates = rentedDates;

    return View(new RentedVehicle { VehicleId = id }); // sadece ID gönderiyoruz
}



    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(RentedVehicle rented)
    {
        var vehicle = _context.Vehicles.FirstOrDefault(v => v.Id == rented.VehicleId);
        var user = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);

        if (vehicle == null || user == null)
            return NotFound();

        
        // Tarih çakışma kontrolü
        bool tarihCakisiyor = _context.RentedVehicles.Any(rv =>
            rv.VehicleId == rented.VehicleId &&
            rented.StartDate < rv.EndDate &&
            rented.EndDate > rv.StartDate
        );

        if (tarihCakisiyor)
        {
            ViewBag.VehiclePrice = vehicle.Price;
            return View(rented);
        }

        rented.UserId = user.Id;
        rented.StartDate = rented.StartDate.Date;
        rented.EndDate = rented.EndDate.Date;
        rented.RentDate = DateTime.Now;

        var totalDays = (rented.EndDate - rented.StartDate).TotalDays;
        rented.TotalPrice = (decimal)totalDays * vehicle.Price;
        rented.Id = 0;

        _context.RentedVehicles.Add(rented);
        _context.SaveChanges();

        return RedirectToAction("Index", "Vehicle");
    }
}
