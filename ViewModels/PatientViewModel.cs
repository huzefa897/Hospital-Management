using HospitalManagementApplication.Models;
using HospitalManagementApplication.Interfaces;
using System.Collections.Generic;

namespace HospitalManagementApplication.ViewModels
{
    public class PatientViewModel
    {
        private readonly IPatientRepository _repo;

        public PatientViewModel(IPatientRepository repo)
        {
            _repo = repo;
        }

        public void AddPatient(Patient patient)
        {
            _repo.AddPatient(patient);
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _repo.GetAll();
        }
    }
}
