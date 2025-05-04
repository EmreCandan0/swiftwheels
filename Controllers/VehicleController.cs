using Microsoft.AspNetCore.Mvc;
using SwiftWheels.Models;
using SwiftWheels.Data;
using SwiftWheels.Models;
using System.Linq;
using System;

namespace SwiftWheels.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehicleController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var vehicles = _context.Vehicles.ToList();
            return View(vehicles);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Vehicles.Add(vehicle);
                _context.SaveChanges();
                return RedirectToAction("Index");
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
        public IActionResult Edit(int id, Vehicle updatedVehicle)
        {
            if (id != updatedVehicle.Id)
            {
                return NotFound();
            }

            var existingVehicle = _context.Vehicles.Find(id);
            if (existingVehicle == null)
            {
                return NotFound();
            }

            // Güncelleme
            existingVehicle.Make = updatedVehicle.Make;
            existingVehicle.Model = updatedVehicle.Model;
            existingVehicle.Year = updatedVehicle.Year;
            existingVehicle.Price = updatedVehicle.Price;
            existingVehicle.Availability = updatedVehicle.Availability;

            // Eğer admin "Available" yaptıysa kiralama kaydını sil
            if (updatedVehicle.Availability)
            {
                var rentals = _context.RentedVehicles
                    .Where(rv => rv.VehicleId == id)
                    .ToList();

                _context.RentedVehicles.RemoveRange(rentals);
            }

            _context.SaveChanges();

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
