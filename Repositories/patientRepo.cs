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

// <----------------Repo Functions--------------------------->
//  Patient AddPatient(Patient patient);
//         Patient? GetById(int id); completed
//         IEnumerable<Patient> GetAll(); completed
//         IEnumerable<Patient> SearchByName(string term); 
//         bool Update(Patient patient); completed
//         bool Delete(int id);
// <----------------Repo Functions--------------------------->




        public bool Update(Patient updated){
            var alreadyExists = GetPatientById(updated.Id);
            if(alreadyExists == null){
                return false;
            }      
            else{
                alreadyExists.Name = updated.Name;
                alreadyExists.Age = updated.Age;
                alreadyExists.Address = updated.Address;
                alreadyExists.phone = updated.phone;
                alreadyExists.email = updated.email;
                return true;
            }      
            
        }   
        public bool Delete(int id){
            var alreadyExists = GetPatientById(id);
            if(alreadyExists == null){
                return false;
            }      
            else{
                _patients.Remove(alreadyExists);
                return true;
            }      
            
        }   

        public Patient AddPatient(Patient p) //Add Pt
        {
            p.Id = _nextId++;
            _patients.Add(p);
            return p;
        }

        public Patient GetPatientById(int id) //Get Pt by ID
        {
            return _patients.FirstOrDefault(p => p.Id == id)!;
        }

        public IEnumerable<Patient> SearchByName(string term)
        {
            List<Patient> returnList =new();
            foreach(var p in _patients){
                if(p.Name.Contains(term, StringComparison.OrdinalIgnoreCase)){
                    returnList.Add(p);
                }
            } 
            return returnList;
        }

        public IEnumerable<Patient> GetAll() //Get all pts
        {
            return _patients;
        }
    }
}
