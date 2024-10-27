using emergencyProject.Models;
using Microsoft.EntityFrameworkCore;

namespace emergencyProject.DataDb;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // DbSet tanımlamaları
    public DbSet<Assistant> Assistants { get; set; } // Asistanlar tablosu
    public DbSet<Professor> Professors { get; set; } // Profesörler tablosu
    public DbSet<Department> Departments { get; set; } // Departmanlar tablosu
    public DbSet<Appointment> Appointments { get; set; } // Randevular tablosu
    public DbSet<Shift> Shifts { get; set; } // Nöbetler tablosu
    public DbSet<User> Users { get; set; } // Kullanıcılar tablosu

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // İlişkilerin yapılandırılması

        modelBuilder.Entity<Assistant>()
             .HasMany(a => a.Appointments)
             .WithOne(a => a.Assistant)
             .HasForeignKey(a => a.AssistantId)
             .OnDelete(DeleteBehavior.Restrict); // Asistan silinirse randevular etkilenmez

        // Professor - Appointment ilişkisi
        modelBuilder.Entity<Professor>()
            .HasMany(p => p.Appointments)
            .WithOne(a => a.Professor)
            .HasForeignKey(a => a.ProfessorId)
            .OnDelete(DeleteBehavior.Restrict); // Profesör silinirse randevular etkilenmez

        // Assistant - Shift ilişkisi
        modelBuilder.Entity<Assistant>()
            .HasMany(a => a.Shifts)
            .WithOne(s => s.Assistant)
            .HasForeignKey(s => s.AssistantId)
            .OnDelete(DeleteBehavior.Restrict); // Asistan silinirse nöbetler etkilenmez

      

        // Department - Professor ilişkisi
        modelBuilder.Entity<Department>()
            .HasMany(d => d.Professors)
            .WithOne(p => p.Department)
            .HasForeignKey(p => p.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade); // Departman silinirse profesörler de silinir

        // Department - Shift ilişkisi
        modelBuilder.Entity<Department>()
            .HasMany(d => d.Shifts)
            .WithOne(s => s.Department)
            .HasForeignKey(s => s.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade); // Departman silinirse nöbetler de silinir

        // Assistant - User ilişkisi (isteğe bağlı)
        modelBuilder.Entity<Assistant>()
            .HasMany(a => a.Users)
            .WithOne(u => u.Assistant)
            .HasForeignKey(u => u.AssistantId)
            .OnDelete(DeleteBehavior.SetNull); // Asistan silinirse kullanıcı asistanı null olur

        // Professor - User ilişkisi (isteğe bağlı)
        modelBuilder.Entity<Professor>()
            .HasMany(p => p.Users)
            .WithOne(u => u.Professor)
            .HasForeignKey(u => u.ProfessorId)
            .OnDelete(DeleteBehavior.SetNull);



    }
}
