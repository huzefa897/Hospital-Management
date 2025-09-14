using Microsoft.EntityFrameworkCore;
using HospitalManagementApplication.Models;

namespace HospitalManagementApplication.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Appointment> Appointments => Set<Appointment>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder b)
        {
            // Patient
            b.Entity<Patient>().HasKey(p => p.Id);
            b.Entity<Patient>().Property(p => p.Name).HasMaxLength(200).IsRequired();
            b.Entity<Patient>().HasIndex(p => p.UserId).IsUnique(false);

            // Doctor
            b.Entity<Doctor>().HasKey(d => d.Id);
            b.Entity<Doctor>().Property(d => d.Name).HasMaxLength(100).IsRequired();
            b.Entity<Doctor>().Property(d => d.Speciality).HasMaxLength(100);
            b.Entity<Doctor>().HasIndex(d => d.UserId).IsUnique(true).HasFilter(null); // unique when set

            // User
            b.Entity<User>().HasKey(u => u.Id);
            b.Entity<User>().HasIndex(u => u.Username).IsUnique();
            // not enforcing FK here to keep it simple; you can add if you want

            // Appointment
            b.Entity<Appointment>().HasKey(a => a.Id);
            b.Entity<Appointment>().HasIndex(a => new { a.DoctorId, a.StartUtc }).IsUnique(); // prevent double-book
            b.Entity<Appointment>().HasIndex(a => a.PatientId);

            // relationships (no cascade delete to be safe)
            b.Entity<Appointment>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            b.Entity<Appointment>()
                .HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
