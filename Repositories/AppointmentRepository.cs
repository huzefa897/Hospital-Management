using Microsoft.EntityFrameworkCore;
using HospitalManagementApplication.Data;
using HospitalManagementApplication.Interfaces;
using HospitalManagementApplication.Models;

namespace HospitalManagementApplication.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _db;
        public AppointmentRepository(AppDbContext db) => _db = db;

        public async Task<Appointment> BookAsync(int patientId, int doctorId, DateTime startUtc, int durationMinutes, string reason)
        {
            // prevent double-book at same StartUtc
            var clash = await _db.Appointments.AnyAsync(a => a.DoctorId == doctorId && a.StartUtc == startUtc && a.Status == AppointmentStatus.Scheduled);
            if (clash) throw new InvalidOperationException("Doctor is already booked at that time.");

            var appt = new Appointment
            {
                PatientId = patientId,
                DoctorId = doctorId,
                StartUtc = startUtc,
                DurationMinutes = durationMinutes,
                Reason = reason,
                Status = AppointmentStatus.Scheduled
            };
            _db.Appointments.Add(appt);
            await _db.SaveChangesAsync();
            return appt;
        }

        public Task<List<Appointment>> ForDoctorAsync(int doctorId, DateTime? fromUtc = null, DateTime? toUtc = null)
        {
            var q = _db.Appointments.AsNoTracking().Where(a => a.DoctorId == doctorId);
            if (fromUtc.HasValue) q = q.Where(a => a.StartUtc >= fromUtc.Value);
            if (toUtc.HasValue)   q = q.Where(a => a.StartUtc <  toUtc.Value);
            return q.OrderBy(a => a.StartUtc).ToListAsync();
        }

        public Task<List<Appointment>> ForPatientAsync(int patientId, DateTime? fromUtc = null, DateTime? toUtc = null)
        {
            var q = _db.Appointments.AsNoTracking().Where(a => a.PatientId == patientId);
            if (fromUtc.HasValue) q = q.Where(a => a.StartUtc >= fromUtc.Value);
            if (toUtc.HasValue)   q = q.Where(a => a.StartUtc <  toUtc.Value);
            return q.OrderBy(a => a.StartUtc).ToListAsync();
        }

        public async Task<bool> CancelAsync(int appointmentId)
        {
            var a = await _db.Appointments.FirstOrDefaultAsync(x => x.Id == appointmentId);
            if (a is null) return false;
            a.Status = AppointmentStatus.Cancelled;
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> CompleteAsync(int appointmentId)
        {
            var a = await _db.Appointments.FirstOrDefaultAsync(x => x.Id == appointmentId);
            if (a is null) return false;
            a.Status = AppointmentStatus.Completed;
            return await _db.SaveChangesAsync() > 0;
        }
        public Task<List<Appointment>> AllAsync(DateTime? fromUtc = null, DateTime? toUtc = null)
            {
                var q = _db.Appointments.AsNoTracking().AsQueryable();
                if (fromUtc.HasValue) q = q.Where(a => a.StartUtc >= fromUtc.Value);
                if (toUtc.HasValue)   q = q.Where(a => a.StartUtc <  toUtc.Value);
                return q.OrderBy(a => a.StartUtc).ToListAsync();
            }

    }
}
