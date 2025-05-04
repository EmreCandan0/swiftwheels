using Microsoft.EntityFrameworkCore;
using SwiftWheels.Models;

namespace SwiftWheels.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RentedVehicle> RentedVehicles { get; set; }
    }
}
