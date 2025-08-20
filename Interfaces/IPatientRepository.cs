using HospitalManagementApplication.Models;
using System.Collections.Generic;

namespace HospitalManagementApplication.Interfaces
{
    public interface IPatientRepository
    {
        Patient GetPatientById(int id);
        Patient AddPatient(Patient patient);
        IEnumerable<Patient> GetAll();
    }
}
