namespace HospitalManagementApplication.ViewModels
{
    public class PatientCreateVM
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Disease { get; set; } = "";
        public int? PrimaryDoctorId { get; set; }
    }
}
