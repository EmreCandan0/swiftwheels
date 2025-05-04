using System;

namespace SwiftWheels.Models
{
    public class RentedVehicle
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime RentDate { get; set; } = DateTime.Now;
    }
}
