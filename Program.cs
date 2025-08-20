using HospitalManagementApplication.Interfaces;
using HospitalManagementApplication.Repositories;
using HospitalManagementApplication.ViewModels;
using HospitalManagementApplication.Views;

namespace HospitalManagementApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IPatientRepository repo = new PatientRepository();
            var viewModel = new PatientViewModel(repo);
            var view = new PatientView(viewModel);
            view.ShowMenu();
        }
    }
}
