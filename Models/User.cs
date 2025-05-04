using System.ComponentModel.DataAnnotations;

namespace SwiftWheels.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
       

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }

        public string Roles { get; set; } // Örn: "ROLE_ADMIN", "ROLE_USER"

        public bool SystemUser { get; set; }
    }
}
