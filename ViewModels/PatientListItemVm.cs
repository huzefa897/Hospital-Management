namespace HospitalManagementApplication.ViewModels
{
    public class PatientListItemVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Disease { get; set; } = "";
        public string? DoctorName { get; set; }

        // Optional constructor to make conversion easier
        public PatientListItemVm() {}

        public PatientListItemVm(Models.Patient p, string? doctorName = null)
        {
            Id = p.Id;
            Name = p.Name;
            Age = p.Age;
            Disease = p.Disease;
            DoctorName = doctorName;
        }
    }
}
