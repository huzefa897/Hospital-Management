using System;

namespace HospitalManagementApplication.Headers
{
    public static class HeaderHelper
    {
        public static void DrawHeader(string title)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("     🏥 Hospital Management System");
            Console.WriteLine("========================================");
            Console.WriteLine($"     \t \t {title}");
            Console.WriteLine("========================================");
            Console.WriteLine();
        }
    }
}
