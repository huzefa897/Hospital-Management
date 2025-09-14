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

        [MaxLength(32)]
        public string Role { get; set; } = "User";
    }
}
