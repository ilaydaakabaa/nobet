using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emergencyProject.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; } // Birincil Anahtar (PK)

        [Required]
        public int AssistantId { get; set; } // İlişkili asistan
        [ForeignKey("AssistantId")]
        public virtual Assistant Assistant { get; set; } = null!; // İlişkili asistan (null referans hatası için!)

        [Required]
        public int ProfessorId { get; set; } // İlişkili profesör
        [ForeignKey("ProfessorId")]
        public virtual Professor Professor { get; set; } = null!; // İlişkili profesör (null referans hatası için!)

        [Required]
        public DateTime AppointmentDate { get; set; } // Randevu tarihi


        [Required]
        public AppointmentStatus Status { get; set; } // Randevu durumu
    }
}
