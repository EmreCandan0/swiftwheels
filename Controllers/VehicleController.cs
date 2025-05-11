using Microsoft.AspNetCore.Mvc;
using SwiftWheels.Models;
using SwiftWheels.Data;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace SwiftWheels.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public VehicleController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            var vehicles = from v in _context.Vehicles
                           select v;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var terms = searchTerm.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var term in terms)
                {
                    vehicles = vehicles.Where(v =>
                        v.Make.ToLower().Contains(term.ToLower()) ||
                        v.Model.ToLower().Contains(term.ToLower()));
                }
            }

            return View(await vehicles.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehicle vehicle, IFormFile photoFile)
        {
            if (ModelState.IsValid)
            {
                if (photoFile != null && photoFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
                    var path = Path.Combine(_env.WebRootPath, "images", "cars");

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        await photoFile.CopyToAsync(stream);
                    }

                    vehicle.ImagePath = "/images/cars/" + fileName;
                }

                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var vehicle = _context.Vehicles.FirstOrDefault(v => v.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vehicle = _context.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vehicle updatedVehicle, IFormFile photoFile)
        {
            if (id != updatedVehicle.Id)
                return NotFound();

            var existingVehicle = _context.Vehicles.Find(id);
            if (existingVehicle == null)
                return NotFound();

            // Fotoğraf güncellemesi
            if (photoFile != null && photoFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
                var path = Path.Combine(_env.WebRootPath, "images", "cars");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    await photoFile.CopyToAsync(stream);
                }

                existingVehicle.ImagePath = "/images/cars/" + fileName;
            }

            // Diğer alanları güncelle
            existingVehicle.VehicleType = updatedVehicle.VehicleType;
            existingVehicle.Make = updatedVehicle.Make;
            existingVehicle.Model = updatedVehicle.Model;
            existingVehicle.Year = updatedVehicle.Year;
            existingVehicle.Km = updatedVehicle.Km;
            existingVehicle.Color = updatedVehicle.Color;
            existingVehicle.EnginePower = updatedVehicle.EnginePower;
            existingVehicle.EngineCapacity = updatedVehicle.EngineCapacity;
            existingVehicle.Price = updatedVehicle.Price;
            existingVehicle.Availability = updatedVehicle.Availability;
            existingVehicle.Fuel = updatedVehicle.Fuel;

            // Eğer admin "Available" yaptıysa kiralama kaydını sil
            if (updatedVehicle.Availability)
            {
                var rentals = _context.RentedVehicles
                    .Where(rv => rv.VehicleId == id)
                    .ToList();

                _context.RentedVehicles.RemoveRange(rentals);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var vehicle = _context.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var vehicle = _context.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            _context.Vehicles.Remove(vehicle);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
