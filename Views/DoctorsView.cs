using System;
using System.Linq;
using System.Threading.Tasks;
using HospitalManagementApplication.Headers;
using HospitalManagementApplication.Interfaces;
using HospitalManagementApplication.Models;

namespace HospitalManagementApplication.Views
{
    public class DoctorsView
    {
        private readonly IDoctorRepository _doctors;

        public DoctorsView(IDoctorRepository doctors) => _doctors = doctors;

        public bool Run()
        {
            while (true)
            {
                Console.Clear();
                HeaderHelper.DrawHeader("Manage Doctors");
                Console.WriteLine("1) Add Doctor");
                Console.WriteLine("2) List Doctors");
                Console.WriteLine("3) Delete Doctor");
                Console.WriteLine("Q) Back");
                Console.Write("\nSelect: ");
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1: Add(); break;
                    case ConsoleKey.D2: List(); break;
                    case ConsoleKey.D3: Delete(); break;
                    case ConsoleKey.Q: return true;
                }
            }
        }

        private void Add()
        {
            Console.Clear();
            HeaderHelper.DrawHeader("Add Doctor");
            Console.Write("Name: ");
            var name = Console.ReadLine() ?? "";
            Console.Write("Speciality: ");
            var spec = Console.ReadLine() ?? "";

            var doc = new Doctor { Name = name, Speciality = spec };
            _doctors.AddAsync(doc).GetAwaiter().GetResult();
            Console.WriteLine("Doctor saved ✅");
            Pause();
        }

      private void List()
{
    Console.Clear();
    HeaderHelper.DrawHeader("Doctors");
    var list = _doctors.GetAllAsync().GetAwaiter().GetResult().OrderBy(d => d.Name).ToList();
    if (!list.Any()) { Console.WriteLine("(none)"); Pause(); return; }

    // Id  Name  Speciality
    Console.WriteLine("Id   Name                   Speciality");
    Console.WriteLine("──── ────────────────────── ──────────────────────");
    foreach (var d in list)
    {
        string name = (d.Name ?? "").PadRight(21);
        string spec = (d.Speciality ?? "").PadRight(21);
        Console.WriteLine($"{d.Id, -4} {name} {spec}");
    }
    Pause();
}

        private void Delete()
        {
            Console.Clear();
            HeaderHelper.DrawHeader("Delete Doctor");
            Console.Write("Doctor Id: ");
            if (!int.TryParse(Console.ReadLine(), out var id)) { Console.WriteLine("Invalid Id"); Pause(); return; }

            var ok = _doctors.DeleteAsync(id).GetAwaiter().GetResult();
            Console.WriteLine(ok ? "Deleted ✅" : "Not found ❌");
            Pause();
        }

        private static void Pause()
        {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }
}
