namespace HospitalManagementApplication.ViewModels
{
    public class PatientEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Address {get;set;}
        public string phone {get;set;}
        public string email {get;set;}
        public int? PrimaryDoctorId { get; set; }
    }
}
