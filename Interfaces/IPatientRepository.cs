using HospitalManagementApplication.Models;
using System.Collections.Generic;

namespace HospitalManagementApplication.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> AddPatient(Patient patient);
        // public Patient? GetById(int id);
         public Task<Patient> GetPatientById(int id);
        public Task<IEnumerable<Patient>> GetAll() ;
        public Task<IEnumerable<Patient>> SearchByName(string term);
        public Task<bool> Update(Patient patient);
        public Task<bool> Delete(int id);
    }
}
