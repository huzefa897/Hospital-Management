// Repositories/DoctorRepository.cs
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HospitalManagementApplication.Data;
using HospitalManagementApplication.Interfaces;
using HospitalManagementApplication.Models;

namespace HospitalManagementApplication.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _db;
        public DoctorRepository(AppDbContext db) => _db = db;

        public Doctor GetDoctorById(int id)
        {
            var doc = _db.Doctors.AsNoTracking().FirstOrDefault(d => d.Id == id);
            if (doc is null) throw new KeyNotFoundException($"Doctor {id} not found.");
            return doc;
        }

        public Doctor AddDoctor(Doctor doctor)
        {
            _db.Doctors.Add(doctor);
            _db.SaveChanges();           // persist
            return doctor;               // Id set by DB
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _db.Doctors
                      .AsNoTracking()
                      .OrderBy(d => d.Name)
                      .ToList();
        }
    }
}
