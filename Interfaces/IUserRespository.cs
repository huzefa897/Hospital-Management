using System.Threading.Tasks;
using HospitalManagementApplication.Models;

namespace HospitalManagementApplication.Interfaces
{
    public interface IUserRepository
    {
        Task<User> RegisterAsync(string username, string password, UserRole role = UserRole.Patient, int? doctorId = null, int? patientId = null);
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> VerifyAsync(string username, string password);
    }
}
