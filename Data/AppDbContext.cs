// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using HospitalManagementApplication.Models;

namespace HospitalManagementApplication.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Doctor>  Doctors  => Set<Doctor>();   // <— add this

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Patient>().HasKey(p => p.Id);
            b.Entity<Patient>().Property(p => p.Name).HasMaxLength(200).IsRequired();

            // optional rules for Doctor
            b.Entity<Doctor>().HasKey(d => d.Id);
            b.Entity<Doctor>().Property(d => d.Name).HasMaxLength(100).IsRequired();
            b.Entity<Doctor>().Property(d => d.Speciality).HasMaxLength(100);
        }
    }
}
