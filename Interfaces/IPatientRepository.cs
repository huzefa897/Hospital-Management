using HospitalManagementApplication.Models;
using System.Collections.Generic;

namespace HospitalManagementApplication.Interfaces
{
    public interface IPatientRepository
    {
        Patient AddPatient(Patient patient);
        // public Patient? GetById(int id);
         public Patient GetPatientById(int id);
        public IEnumerable<Patient> GetAll() ;
        IEnumerable<Patient> SearchByName(string term);
        bool Update(Patient patient);
        bool Delete(int id);
    }
}
