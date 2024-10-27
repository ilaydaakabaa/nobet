using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emergencyProject.Models
{
    public class Shift
    {
        [Key]
        public int ShiftId { get; set; } // Birincil Anahtar (PK)

        [Required]
        public int AssistantId { get; set; } // İlişkili asistan
        [ForeignKey("AssistantId")]
        public virtual Assistant Assistant { get; set; } = null!; // İlişkili asistan (null referans hatası için!)

        [Required]
        public int DepartmentId { get; set; } // İlişkili departman
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; } = null!; // İlişkili departman (null referans hatası için!)

        [Required]
        public DateTime ShiftDate { get; set; } // Nöbet tarihi
        [Required]
        public TimeSpan StartTime { get; set; } // Nöbet başlangıç saati
        [Required]
        public TimeSpan EndTime { get; set; } // Nöbet bitiş saati
    }
}
