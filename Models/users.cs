using System.ComponentModel.DataAnnotations;

namespace HospitalManagementApplication.Models
{
    public class User
    {
        [Key] public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; } = "";

        [Required] 
        public string PasswordHash { get; set; } = "";

        public UserRole Role { get; set; } = UserRole.Patient;

        
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
    }
}
