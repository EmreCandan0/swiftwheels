using System.ComponentModel.DataAnnotations;

namespace SwiftWheels.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name = "Mevcut Şifre")]
        public string CurrentPassword { get; set; }

        [Required]
        [Display(Name = "Yeni Şifre")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Şifreler uyuşmuyor.")]
        [Display(Name = "Yeni Şifre (Tekrar)")]
        public string ConfirmPassword { get; set; }
    }
}
