using System;
using System.Collections.Generic;
using HospitalManagementApplication.Models;
using HospitalManagementApplication.Interfaces;
using HospitalManagementApplication.ViewModels;

namespace HospitalManagementApplication.Controllers
{
    public class PatientController
    {
        private readonly IPatientRepository _repo;

        public PatientController(IPatientRepository repo) => _repo = repo;

        public PatientListItemVm Create(PatientCreateVM vm)
        {
            var patient = new Patient
            {
                Name    = vm.Name,
                Age     = vm.Age,
                Address = vm.Address,
                phone   = vm.phone,   // model uses lowercase
                email   = vm.email
            };

            var saved = _repo.AddPatient(patient).GetAwaiter().GetResult();  // <-- sync wrapper

            return new PatientListItemVm
            {
                Id      = saved.Id,
                Name    = saved.Name,
                Age     = saved.Age,
                Address = saved.Address,
                phone   = saved.phone,
                email   = saved.email,
                DoctorName = "N/A"
            };
        }

        public Patient? GetById(int id)
        {
            try
            {
                return _repo.GetPatientById(id).GetAwaiter().GetResult();     // <-- sync wrapper
            }
            catch
            {
                return null;
            }
        }

        public PatientEditVM? GetForEdit(int id)
        {
            if (id == 0) return null;

            var p = GetById(id); // uses the method above
            if (p == null) return null;

            return new PatientEditVM
            {
                Id      = p.Id,
                Name    = p.Name,
                Age     = p.Age,
                Address = p.Address,
                phone   = p.phone,
                email   = p.email,
                PrimaryDoctorId = p.primaryDoctorId
            };
        }

        public IEnumerable<PatientListItemVm> GetAllPatients()
        {
            var list = new List<PatientListItemVm>();
            var patients = _repo.GetAll().GetAwaiter().GetResult();           // <-- sync wrapper

            foreach (var p in patients)
            {
                list.Add(new PatientListItemVm
                {
                    Id      = p.Id,
                    Name    = p.Name,
                    Age     = p.Age,
                    Address = p.Address,
                    phone   = p.phone,
                    email   = p.email,
                    DoctorName = "N/A"
                });
            }
            return list;
        }

        public IEnumerable<PatientListItemVm> SearchByName(string term)
        {
            var list = new List<PatientListItemVm>();
            var results = _repo.SearchByName(term).GetAwaiter().GetResult();  // <-- sync wrapper

            foreach (var p in results)
            {
                list.Add(new PatientListItemVm
                {
                    Id      = p.Id,
                    Name    = p.Name,
                    Age     = p.Age,
                    Address = p.Address,
                    phone   = p.phone,
                    email   = p.email,
                    DoctorName = "N/A"
                });
            }
            return list;
        }

        public bool Update(PatientEditVM vm)
        {
            var existing = GetById(vm.Id);
            if (existing == null) return false;

            existing.Name    = vm.Name;
            existing.Age     = vm.Age;
            existing.Address = vm.Address;
            existing.phone   = vm.phone;
            existing.email   = vm.email;
            existing.primaryDoctorId = vm.PrimaryDoctorId;

            return _repo.Update(existing).GetAwaiter().GetResult();          // <-- sync wrapper
        }

        public bool Delete(int id) =>
            _repo.Delete(id).GetAwaiter().GetResult();                        // <-- sync wrapper
    }
}
