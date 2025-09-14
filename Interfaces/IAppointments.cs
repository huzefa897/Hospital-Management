using HospitalManagementApplication.Models;

namespace HospitalManagementApplication.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment> BookAsync(int patientId, int doctorId, DateTime startUtc, int durationMinutes, string reason);
        Task<List<Appointment>> ForDoctorAsync(int doctorId, DateTime? fromUtc = null, DateTime? toUtc = null);
        Task<List<Appointment>> ForPatientAsync(int patientId, DateTime? fromUtc = null, DateTime? toUtc = null);

        // NEW:
        Task<List<Appointment>> AllAsync(DateTime? fromUtc = null, DateTime? toUtc = null);

        Task<bool> CancelAsync(int appointmentId);
        Task<bool> CompleteAsync(int appointmentId);
    }
}
