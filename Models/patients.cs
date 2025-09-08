namespace HospitalManagementApplication.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Address { get; set; }
        public string phone     {get;set;}
        public string email {get;set;}
        // public string Disease { get; set; } = "";
        public int? primaryDoctorId { get; set; }
    }
}
