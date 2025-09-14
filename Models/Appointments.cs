using System.ComponentModel.DataAnnotations;

namespace HospitalManagementApplication.Models
{
    public enum AppointmentStatus { Scheduled = 1, Completed = 2, Cancelled = 3 }

    public class Appointment
    {
        [Key] public int Id { get; set; }
        [Required] public int PatientId { get; set; }
        [Required] public int DoctorId { get; set; }

        [Required] public DateTime StartUtc { get; set; }
        public int DurationMinutes { get; set; } = 30;

        [MaxLength(200)] public string Reason { get; set; } = "";
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    }
}
