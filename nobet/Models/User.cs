using Microsoft.EntityFrameworkCore;
using nobet.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emergencyProject.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; } // PK

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Şifre boşluk içeremez.")]
        public string Password { get; set; } = null!; // Null referans hatası için

        [Required(ErrorMessage = "Email adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email giriniz.")]
        public string Email { get; set; } = null!; // Null referans hatası için

        // Rol özelliği
        [Required]
        public UserRole Role { get; set; }

        public string? PhoneNumber { get; set; } // İsteğe bağlı
        public string? Address { get; set; } // İsteğe bağlı

        public int? AssistantId { get; set; } // Asistan ile ilişkili (isteğe bağlı)
        public virtual Assistant? Assistant { get; set; } // İlişkili asistan (isteğe bağlı)

        public int? ProfessorId { get; set; } // Profesör ile ilişkili (isteğe bağlı)
        public virtual Professor? Professor { get; set; } // İlişkili profesör (isteğe bağlı)

    }
}
