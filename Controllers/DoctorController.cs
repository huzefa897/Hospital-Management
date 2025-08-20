// using HospitalManagementApplication.Models;
// using HospitalManagementApplication.Interfaces;
// using HospitalManagementApplication.ViewModels;
// using System.Collections.Generic;

// namespace HospitalManagementApplication.Controllers
// {
//     public class DoctorController
//     {
//         private readonly IDoctorRepository _repo;

//         public DoctorController(IDoctorRepository repo)
//         {
//             _repo = repo;
//         }

//         // public DoctorListItemVm Create(DoctorCreateVm vm)
//         // {
//         //     var doctor = new Doctor
//         //     {
//         //         Name = vm.Name,
//         //         Age = vm.Age,
//         //         Disease = vm.Disease,
//         //         primaryDoctorId = vm.PrimaryDoctorId
//         //     };

//         //     var saved = _repo.AddDoctor(doctor);

//         //     return new DoctorListItemVm
//         //     {
//         //         Id = saved.Id,
//         //         Name = saved.Name,
//         //         Age = saved.Age,
//         //         Disease = saved.Disease,
//         //         DoctorName = "N/A" // or fetch from doctor service later
//         //     };
//         // }

//         // public Doctor? GetById(int id)
//         // {
//         //     return _repo.GetById(id);
//         // }

//         // public DoctorEditVm? GetForEdit(int id)
//         // {
//         //     var p = _repo.GetById(id);
//         //     if (p == null) return null;

//         //     return new DoctorEditVm
//         //     {
//         //         Id = p.Id,
//         //         Name = p.Name,
//         //         Age = p.Age,
//         //         Disease = p.Disease,
//         //         PrimaryDoctorId = p.primaryDoctorId
//         //     };
//         // }

//         // public IEnumerable<DoctorListItemVm> GetAllDoctors()
//         // {
//         //     var list = new List<DoctorListItemVm>();
//         //     foreach (var p in _repo.GetAll())
//         //     {
//         //         list.Add(new DoctorListItemVm
//         //         {
//         //             Id = p.Id,
//         //             Name = p.Name,
//         //             Age = p.Age,
//         //             Disease = p.Disease,
//         //             DoctorName = "N/A" // placeholder or fetch if needed
//         //         });
//         //     }
//         //     return list;
//         // }

//         // public IEnumerable<DoctorListItemVm> SearchByName(string term)
//         // {
//         //     var list = new List<DoctorListItemVm>();
//         //     foreach (var p in _repo.SearchByName(term))
//         //     {
//         //         list.Add(new DoctorListItemVm
//         //         {
//         //             Id = p.Id,
//         //             Name = p.Name,
//         //             Age = p.Age,
//         //             Disease = p.Disease,
//         //             DoctorName = "N/A"
//         //         });
//         //     }
//         //     return list;
//         // }

//         // public bool Update(DoctorEditVm vm)
//         // {
//         //     var existing = _repo.GetById(vm.Id);
//         //     if (existing == null) return false;

//         //     existing.Name = vm.Name;
//         //     existing.Age = vm.Age;
//         //     existing.Disease = vm.Disease;
//         //     existing.primaryDoctorId = vm.PrimaryDoctorId;

//         //     return _repo.Update(existing);
//         // }

//         // public bool Delete(int id)
//         // {
//         //     return _repo.Delete(id);
//         // }
//     }
// }
