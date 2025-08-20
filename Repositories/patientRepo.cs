using System.Collections.Generic;
using System.Linq;
using HospitalManagementApplication.Models;
using HospitalManagementApplication.Interfaces;

namespace HospitalManagementApplication.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly List<Patient> _patients = new();
        private int _nextId = 1;

        public Patient AddPatient(Patient p)
        {
            p.Id = _nextId++;
            _patients.Add(p);
            return p;
        }

        public Patient GetPatientById(int id)
        {
            return _patients.FirstOrDefault(p => p.Id == id)!;
        }

        public IEnumerable<Patient> GetAll()
        {
            return _patients;
        }
    }
}
