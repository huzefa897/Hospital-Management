using System.Linq;
using Microsoft.EntityFrameworkCore;
using HospitalManagementApplication.Data;
using HospitalManagementApplication.Interfaces;
using HospitalManagementApplication.Models;
using HospitalManagementApplication.Repositories;
using HospitalManagementApplication.Controllers;
using HospitalManagementApplication.Views;
using HospitalManagementApplication.Config; 
class Program
{
    static void Main(string[] args)
    {
        var dbPath = Paths.GetDatabasePath();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite($"Data Source={dbPath}").Options;
        using var db = new AppDbContext(options);
        db.Database.Migrate();

        var userRepo    = new UserRepository(db);
        var doctorRepo  = new DoctorRepository(db);
        var patientRepo = new PatientRepository(db);
        var apptRepo    = new AppointmentRepository(db);

        // Seed basics
        if (!db.Users.Any())
        {
            var d = doctorRepo.AddAsync(new Doctor { Name = "Dr. Nova", Speciality = "General" }).GetAwaiter().GetResult();
            userRepo.RegisterAsync("admin", "admin123", UserRole.Admin).GetAwaiter().GetResult();
            userRepo.RegisterAsync("drnova", "password", UserRole.Doctor, doctorId: d.Id).GetAwaiter().GetResult();
        }

        var patientController = new PatientController(patientRepo);
        var patientView = new PatientView(patientController);
        var doctorsView = new DoctorsView(doctorRepo);
        var apptView    = new AppointmentView(apptRepo, doctorRepo, patientRepo);

        var loginView = new LoginView(patientView, userRepo, doctorsView, apptView, doctorRepo, patientRepo);

        while (true)
        {
            if (!loginView.Run()) break;
        }
    }
}
