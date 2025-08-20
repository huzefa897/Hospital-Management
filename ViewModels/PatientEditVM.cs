namespace HospitalManagementApplication.ViewModels
{
    public class PatientEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Disease { get; set; } = "";
        public int? PrimaryDoctorId { get; set; }
    }
}
