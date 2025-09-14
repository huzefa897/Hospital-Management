using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HospitalManagementApplication.Data;
using HospitalManagementApplication.Interfaces;
using HospitalManagementApplication.Models;
using HospitalManagementApplication.Util;

namespace HospitalManagementApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) => _db = db;

        public async Task<User> RegisterAsync(string username, string password, string role = "User")
        {
            // prevent duplicates
            if (await _db.Users.AnyAsync(u => u.Username == username))
                throw new InvalidOperationException("Username already exists.");

            var user = new User
            {
                Username = username,
                PasswordHash = PasswordHasher.Hash(password),
                Role = role
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public Task<User?> GetByUsernameAsync(string username) =>
            _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);

        public async Task<bool> VerifyAsync(string username, string password)
        {
            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
            if (user is null) return false;
            return PasswordHasher.Verify(password, user.PasswordHash);
        }
    }
}
