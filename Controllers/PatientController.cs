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
                // Disease = vm.Disease,
                Address = vm.Address,
                phone = vm.phone,
                email = vm.email,
                primaryDoctorId = vm.PrimaryDoctorId
            };

            var saved = _repo.AddPatient(patient);

            return new PatientListItemVm
            {
                Id = saved.Id,
                Name = saved.Name,
                Age = saved.Age,
                Address = saved.Address,
                phone = saved.phone,
                email = saved.email,
                // primaryDoctorId = saved.primaryDoctorId,
                DoctorName = "N/A" // or fetch from doctor service later
            };
        }

        public Patient? GetById(int id)
        {
            return _repo.GetPatientById(id);
        }

        public PatientEditVM? GetForEdit(int id)
        {
            if(id == 0){
                return null;
            }
            var p = _repo.GetPatientById(id);
            if (p == null) return null;

            return new PatientEditVM
            {
                Id = p.Id,
                Name = p.Name,
                Age = p.Age,
                Address = p.Address,
                phone = p.phone,
                email = p.email,
                // primaryDoctorId = p.primaryDoctorId
            };
        }

        //  public PatientEditVM? GetForEdit(string name)
        // {
        //     if(name == ""){
        //         return null;
        //     }
        //     var p = _repo.GetPatientByName(name);
        //     if (p == null) return null;

        //     return new PatientEditVM
        //     {
        //         Id = p.Id,
        //         Name = p.Name,
        //         Age = p.Age,
        //         Disease = p.Disease,
        //         PrimaryDoctorId = p.primaryDoctorId
        //     };
        // }

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
                    Address = p.Address,
                    phone = p.phone,
                    email = p.email,
                    // primaryDoctorId = p.primaryDoctorId,
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
                    Address = p.Address,
                    phone = p.phone,
                    email = p.email,
                    // primaryDoctorId = p.primaryDoctorId,
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
            existing.Address = vm.Address;
            existing.phone = vm.phone;
            existing.email = vm.email;
            existing.primaryDoctorId = vm.PrimaryDoctorId;

            return _repo.Update(existing);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}
