using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emergencyProject.Models
{
    public class Professor
    {
        [Key]
        public int ProfessorId { get; set; } // Birincil Anahtar (PK)

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } // İsim

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } // Soyisim

      

        [Required]
        public int DepartmentId { get; set; } // Departman ile ilişki
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; } = null!; // Departman nesnesi (null referans hatası için!)

        public string? Address { get; set; } // Adres, isteğe bağlı
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>(); // Randevular

        public virtual ICollection<User> Users { get; set; } = new List<User>(); // İlişkili kullanıcılar
    }
}
