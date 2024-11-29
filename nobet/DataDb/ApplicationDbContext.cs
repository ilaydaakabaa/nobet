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
    public DbSet<EmergencyNews> EmergencyNews { get; set; } // Acil durumlar

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Assistant - Appointment ilişkisi
        modelBuilder.Entity<Assistant>()
            .HasMany(a => a.Appointments)
            .WithOne(ap => ap.Assistant)
            .HasForeignKey(ap => ap.AssistantId)
            .OnDelete(DeleteBehavior.NoAction); // Silme davranışı etkisizleştirildi

        // Professor - Appointment ilişkisi
        modelBuilder.Entity<Professor>()
            .HasMany(p => p.Appointments)
            .WithOne(ap => ap.Professor)
            .HasForeignKey(ap => ap.ProfessorId)
            .OnDelete(DeleteBehavior.NoAction); // Silme davranışı etkisizleştirildi

        // Assistant - Shift ilişkisi
        modelBuilder.Entity<Assistant>()
            .HasMany(a => a.Shifts)
            .WithOne(s => s.Assistant)
            .HasForeignKey(s => s.AssistantId)
            .OnDelete(DeleteBehavior.NoAction); // Silme davranışı etkisizleştirildi

        // Department - Professor ilişkisi
        modelBuilder.Entity<Department>()
            .HasMany(d => d.Professors)
            .WithOne(p => p.Department)
            .HasForeignKey(p => p.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction); // Silme davranışı etkisizleştirildi

        // Department - Shift ilişkisi
        modelBuilder.Entity<Department>()
            .HasMany(d => d.Shifts)
            .WithOne(s => s.Department)
            .HasForeignKey(s => s.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction); // Silme davranışı etkisizleştirildi

        // User - Assistant ilişkisi
        modelBuilder.Entity<User>()
            .HasOne(u => u.Assistant)
            .WithOne(a => a.User)
            .HasForeignKey<Assistant>(a => a.UserId)
            .OnDelete(DeleteBehavior.NoAction); // Silme davranışı etkisizleştirildi

        // User - Professor ilişkisi
        modelBuilder.Entity<User>()
            .HasOne(u => u.Professor)
            .WithOne(p => p.User)
            .HasForeignKey<Professor>(p => p.UserId)
            .OnDelete(DeleteBehavior.NoAction); // Silme davranışı etkisizleştirildi
    }

}
