using System.ComponentModel.DataAnnotations;

namespace SwiftWheels.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public string VehicleType { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public int Year { get; set; }

        public int Km { get; set; }

        public string Color { get; set; }

        public string EnginePower { get; set; }

        public string EngineCapacity { get; set; }

        public int Price { get; set; }

        public bool Availability { get; set; }

        public string Fuel { get; set; }

        public string ImagePath { get; set; }
    }
}
