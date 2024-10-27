using System.ComponentModel.DataAnnotations;

namespace emergencyProject.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; } // Birincil Anahtar (PK)

        [Required]
        [StringLength(100)]
        public string Name { get; set; } // Departman adı

        public string? Description { get; set; } // Açıklama, isteğe bağlı
        public int PatientCapacity { get; set; } // Hasta kapasitesi
        public int EmptyBeds { get; set; } // Boş yatak sayısı

        public virtual ICollection<Assistant> Assistants { get; set; } = new List<Assistant>(); // İlişkili asistanlar
        public virtual ICollection<Professor> Professors { get; set; } = new List<Professor>(); // İlişkili profesörler
        public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>(); // İlişkili nöbetler
    }
}
