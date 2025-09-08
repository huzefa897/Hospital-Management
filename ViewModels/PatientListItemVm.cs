namespace HospitalManagementApplication.ViewModels
{
    public class PatientListItemVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Address { get; set; } = "";
        public string phone { get; set; } = "";
        public string email { get; set; } = "";
        public string? DoctorName { get; set; }

        // Optional constructor to make conversion easier
        public PatientListItemVm() {}

        public PatientListItemVm(Models.Patient p, string? doctorName = null)
        {
            Id = p.Id;
            Name = p.Name;
            Age = p.Age;
            Address = p.Address;
            phone = p.phone;
            email = p.email;
            DoctorName = doctorName;
        }
    }
}
