using Microsoft.EntityFrameworkCore;
using nobet.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emergencyProject.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Şifre boşluk içeremez.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Email adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email giriniz.")]
        public string Email { get; set; } = null!;

        [Required]
        public UserRole Role { get; set; } // Kullanıcı Rolü (Assistant veya Professor)

        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public virtual Assistant? Assistant { get; set; } // Role bağlı Assistant ilişkisi
        public virtual Professor? Professor { get; set; } // Role bağlı Professor ilişkisi

    }
}
