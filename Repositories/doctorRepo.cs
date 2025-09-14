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

        public Task<Doctor?> GetByIdAsync(int id) =>
            _db.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);

        public Task<Doctor?> GetByUserIdAsync(int userId) =>
            _db.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.UserId == userId);

        public Task<List<Doctor>> GetAllAsync() =>
            _db.Doctors.AsNoTracking().OrderBy(d => d.Name).ToListAsync();

        public async Task<Doctor> AddAsync(Doctor doctor)
        {
            await _db.Doctors.AddAsync(doctor);
            await _db.SaveChangesAsync();
            return doctor;
        }

        public async Task<bool> UpdateAsync(Doctor doctor)
        {
            var existing = await _db.Doctors.FirstOrDefaultAsync(d => d.Id == doctor.Id);
            if (existing is null) return false;
            existing.Name = doctor.Name;
            existing.Speciality = doctor.Speciality;
            existing.UserId = doctor.UserId;
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (existing is null) return false;
            _db.Doctors.Remove(existing);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
