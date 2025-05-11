using System.ComponentModel.DataAnnotations;

namespace SwiftWheels.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad gerekli.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad gerekli.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "E-posta gerekli.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarası girin.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Şifre gerekli.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Password { get; set; }

        public string Roles { get; set; }

        public bool SystemUser { get; set; }
    }
}
