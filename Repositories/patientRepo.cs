using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HospitalManagementApplication.Models;
using HospitalManagementApplication.Interfaces;
using HospitalManagementApplication.Data;

namespace HospitalManagementApplication.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _db;
        public PatientRepository(AppDbContext db) => _db = db;

        public async Task<Patient> AddPatient(Patient patient)
        {
            await _db.Patients.AddAsync(patient);
            await _db.SaveChangesAsync();
            return patient; // Id populated by DB
        }

        public async Task<Patient> GetPatientById(int id)
        {
            var p = await _db.Patients.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (p is null) throw new KeyNotFoundException($"Patient {id} not found.");
            return p;
        }

        public async Task<IEnumerable<Patient>> GetAll()
        {
            return await _db.Patients
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Patient>> SearchByName(string term)
        {
            term ??= string.Empty;
            return await _db.Patients
                .AsNoTracking()
                .Where(p => EF.Functions.Like(p.Name, $"%{term}%"))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<bool> Update(Patient updated)
        {
            var existing = await _db.Patients.FirstOrDefaultAsync(x => x.Id == updated.Id);
            if (existing is null) return false;

            // Map allowed fields (use PascalCase property names)
            existing.Name    = updated.Name;
            existing.Age     = updated.Age;
            existing.Address = updated.Address;
            existing.phone   = updated.phone;
            existing.email   = updated.email;

            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await _db.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (existing is null) return false;

            _db.Patients.Remove(existing);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
