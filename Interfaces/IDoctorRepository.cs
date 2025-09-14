using HospitalManagementApplication.Models;
using System.Collections.Generic;

namespace HospitalManagementApplication.Interfaces
{
    public interface IDoctorRepository
    {
        Doctor GetDoctorById(int id);
        Doctor AddDoctor(Doctor doctor);
        IEnumerable<Doctor> GetAll();
    }
}
