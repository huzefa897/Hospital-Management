namespace HospitalManagementApplication.ViewModels
{
   public class PatientCreateVM
{
    public string Name    { get; set; } = "";
    public int Age        { get; set; }
    public string Address { get; set; } = "";  // ← add
    public string email   { get; set; } = "";  // ← add
    public string phone   { get; set; } = "";  // ← add
}

}
