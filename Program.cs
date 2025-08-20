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
            IPatientRepository repo = new PatientRepository();
            var controller = new PatientController(repo); 
            var patientView = new PatientView(controller); 

            var homeView = new HomeView(patientView, null!); // null-forgiving operator
            homeView.Run();
        }
    }
}
