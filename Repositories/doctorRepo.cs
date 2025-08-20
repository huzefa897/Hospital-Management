// using System.Collections.Generic;
// using System.Linq;
// using HospitalManagementApplication.Models;
// using HospitalManagementApplication.Interfaces;

// namespace HospitalManagementApplication.Repositories
// {
//     public class DoctorRepository : IDoctorRepository
//     {
//         private readonly List<Doctor> _doctors = new();
//         private int _nextId = 1;

        
// // <----------------Repo Functions--------------------------->
// //  Patient AddPatient(Patient patient);
// //         Patient? GetById(int id); completed
// //         IEnumerable<Patient> GetAll(); completed
// //         IEnumerable<Patient> SearchByName(string term); 
// //         bool Update(Patient patient); completed
// //         bool Delete(int id);
// // <----------------Repo Functions--------------------------->



//         public bool Update(Doctor updated){
//             // var alreadyExists = GetDoctorById(updated.Id);
//             // if(alreadyExists == null){
//             //     return false;
//             // }      
//             // else{
//             //     alreadyExists.Name = updated.Name;
//             //     alreadyExists.Age = updated.Age;
//             //     alreadyExists.Disease = updated.Disease;
//             //     return true;
//             // }      
//             return;
            
//         }   
//         public bool Delete(int id){
//             // var alreadyExists = GetDoctorById(id);
//             // if(alreadyExists == null){
//             //     return false;
//             // }      
//             // else{
//             //     _doctors.Remove(alreadyExists);
//             //     return true;
//             // }      
//             return;
//         }   

//         public Doctor AddDoctor(Doctor d) //Add Pt
//         {
//             // d.DocId = _nextId++;
//             // _doctors.Add(d);
//             // return d;
//             return;
//         }

//         public Doctor GetDoctorById(int id) //Get Pt by ID
//         {
//             // return _doctors.FirstOrDefault(p => p.Id == id)!;
//             return;
//         }

//         public IEnumerable<Doctor> GetAll() //Get all pts
//         {
//             // return _doctors;
//             return;
//         }
//     }
// }

                