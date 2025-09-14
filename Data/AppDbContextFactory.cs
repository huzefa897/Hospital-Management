using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HospitalManagementApplication.Data
{
    // Used by "dotnet ef" to create the DbContext at design-time
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Data Source=hospital.db")
                .Options;

            return new AppDbContext(options);
        }
    }
}
