using System.ComponentModel.DataAnnotations;

namespace HospitalManagementApplication.Models
{
    public class Patient
    {
        [Key]public int Id { get; set; }
        [Required,MaxLength(100)]public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Address { get; set; } = "";
        public string phone     {get;set;} = "";
        public string email {get;set;} = "";
        // public string Disease { get; set; } = "";
        public int? primaryDoctorId { get; set; }
        public int? UserId { get; set; }
    }
    
}
