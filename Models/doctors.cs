// Models/doctors.cs
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementApplication.Models
{
    public class Doctor
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(100)] public string Name { get; set; } = "";
        [MaxLength(100)] public string Speciality { get; set; } = "";
    }
}
