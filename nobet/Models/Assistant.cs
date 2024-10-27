using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emergencyProject.Models
{
    public class Assistant
    {
        [Key]
        public int AssistantId { get; set; } // Birincil Anahtar (PK)

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } // İsim

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } // Soyisim

     

        public ICollection<Appointment> Appointments { get; set; } // Randevular
        public ICollection<Shift> Shifts { get; set; } // Nöbetler

        public virtual ICollection<User> Users { get; set; } = new List<User>(); // İlişkili kullanıcılar
    }
}
