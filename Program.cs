using Microsoft.EntityFrameworkCore;
using HospitalManagementApplication.Data;
using HospitalManagementApplication.Interfaces;
using HospitalManagementApplication.Repositories;
using HospitalManagementApplication.Controllers;
using HospitalManagementApplication.Views;

namespace HospitalManagementApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Data Source=hospital.db")
                .Options;

            using var db = new AppDbContext(options);

            // EITHER use migrations:
            db.Database.Migrate();
            // OR, for a quick start without migrations, use:
            // db.Database.EnsureCreated();  // (don’t mix with Migrate later)

            IPatientRepository repo = new PatientRepository(db);
            var controller = new PatientController(repo);
            var patientView = new PatientView(controller);

            var loginView = new LoginView(patientView, null);

            while (true)
            {
                bool user = loginView.Run();
                if (!user) break;

                var homeView = new HomeView(patientView, null, "user");
                bool loggedOut = homeView.Run();
                if (!loggedOut) break;
            }
        }
    }
}
