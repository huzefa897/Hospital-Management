using System;
using System.IO;

namespace HospitalManagementApplication.Config
{
    public static class Paths
    {
        
        public static string GetAppDataDir()
        {
            var root = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            // Windows: C:\Users\<you>\AppData\Local
            // macOS:   /Users/<you>/.local/share  (or similar, depending on OS)
            var dir = Path.Combine(root, "HospitalManagementApp");
            Directory.CreateDirectory(dir);
            return dir;
        }

        public static string GetDatabasePath() =>
            Path.Combine(GetAppDataDir(), "hospital.db");
    }
}
