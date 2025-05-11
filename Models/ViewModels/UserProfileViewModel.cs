using System.ComponentModel.DataAnnotations;

namespace SwiftWheels.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Display(Name = "Soyad")]
        public string Surname { get; set; }

        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }

        [Display(Name = "E-posta")]
        public string Email { get; set; }
    }
}
