namespace HospitalManagementApplication.ViewModels
{
    public class PatientCreateVM
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Address { get; set; }
        public string email {get;set;}
        public string phone     {get;set;}
        // public string Disease { get; set; } = "";
        public int? PrimaryDoctorId { get; set; }
    }
}
