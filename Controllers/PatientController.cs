using HospitalManagementApplication.Models;
using HospitalManagementApplication.Interfaces;
using HospitalManagementApplication.ViewModels;

using System.Collections.Generic;

namespace HospitalManagementApplication.Controllers
{
    public class PatientController
    {
        private readonly IPatientRepository _repo;

        public PatientController(IPatientRepository repo)
        {
            _repo = repo;
        }

        public PatientListItemVm Create(PatientCreateVM vm)
        {
            var patient = new Patient
            {
                Name = vm.Name,
                Age = vm.Age,
                Disease = vm.Disease,
                primaryDoctorId = vm.PrimaryDoctorId
            };

            var saved = _repo.AddPatient(patient);

            return new PatientListItemVm
            {
                Id = saved.Id,
                Name = saved.Name,
                Age = saved.Age,
                Disease = saved.Disease,
                DoctorName = "N/A" // or fetch from doctor service later
            };
        }

        public Patient? GetById(int id)
        {
            return _repo.GetPatientById(id);
        }

        public PatientEditVM? GetForEdit(int id)
        {
            var p = _repo.GetPatientById(id);
            if (p == null) return null;

            return new PatientEditVM
            {
                Id = p.Id,
                Name = p.Name,
                Age = p.Age,
                Disease = p.Disease,
                PrimaryDoctorId = p.primaryDoctorId
            };
        }

        public IEnumerable<PatientListItemVm> GetAllPatients()
        {
            var list = new List<PatientListItemVm>();
            foreach (var p in _repo.GetAll())
            {
                list.Add(new PatientListItemVm
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    Disease = p.Disease,
                    DoctorName = "N/A" // placeholder or fetch if needed
                });
            }
            return list;
        }

        public IEnumerable<PatientListItemVm> SearchByName(string term)
        {
            var list = new List<PatientListItemVm>();
            foreach (var p in _repo.SearchByName(term))
            {
                list.Add(new PatientListItemVm
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    Disease = p.Disease,
                    DoctorName = "N/A"
                });
            }
            return list;
        }

        public bool Update(PatientEditVM vm)
        {
            var existing = _repo.GetPatientById(vm.Id);
            if (existing == null) return false;

            existing.Name = vm.Name;
            existing.Age = vm.Age;
            existing.Disease = vm.Disease;
            existing.primaryDoctorId = vm.PrimaryDoctorId;

            return _repo.Update(existing);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}
