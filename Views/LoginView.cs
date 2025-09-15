using System;
using HospitalManagementApplication.Headers;
using HospitalManagementApplication.Interfaces;
using HospitalManagementApplication.Models;

namespace HospitalManagementApplication.Views
{
    public class LoginView
    {
        private readonly PatientView _patientView;
        private readonly DoctorsView _doctorsView;
        private readonly AppointmentView _apptView;
        private readonly IUserRepository _users;
        private readonly IDoctorRepository _doctorRepo;
        private readonly IPatientRepository _patientRepo;

        private string? _currentUser;
        private UserRole _currentRole;
        private int? _currentDoctorId;
        private int? _currentPatientId;

        public LoginView(
            PatientView patientView,
            IUserRepository users,
            DoctorsView doctorsView,
            AppointmentView apptView,
            IDoctorRepository doctorRepo,
            IPatientRepository patientRepo)
        {
            _patientView = patientView;
            _users = users;
            _doctorsView = doctorsView;
            _apptView = apptView;
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
        }

        public bool Run()
        {
            while (true)
            {
                if (_currentUser == null)
                {
                    if (!ShowLoginPage()) return false;
                }
                else
                {
                    var home = new HomeView(_patientView, _doctorsView, _apptView, _currentUser!, _currentRole, _currentDoctorId, _currentPatientId);
                    var loggedOut = home.Run();
                    if (loggedOut) { _currentUser = null; _currentDoctorId = null; _currentPatientId = null; }
                    else return false;
                }
            }
        }

        private bool ShowLoginPage()
        {
            Console.Clear();
            HeaderHelper.DrawHeader("User Options");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("Q. Exit");
            Console.Write("\nSelect: ");
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.D1: Register(); return true;
                case ConsoleKey.D2: return Login();
                case ConsoleKey.Q:  return false;
                default:            return true;
            }
        }

        private void Register()
        {
            Console.Clear();
            HeaderHelper.DrawHeader("User Registration");
            Console.Write("Username: ");
            var uname = Console.ReadLine() ?? "";
            Console.Write("Password: ");
            var pwd = ReadPassword();

            Console.WriteLine("\nRole: 1) Admin  2) Doctor  3) Patient");
            Console.Write("Select role: ");
            var roleKey = Console.ReadKey(true).Key;
            var role = roleKey switch
            {
                ConsoleKey.D1 => UserRole.Admin,
                ConsoleKey.D2 => UserRole.Doctor,
                _             => UserRole.Patient
            };

            int? doctorId = null;
            int? patientId = null;

            try
            {
                if (role == UserRole.Doctor)
                {
                    Console.Write("\nDoctor Name: ");
                    var dn = Console.ReadLine() ?? "";
                    Console.Write("Speciality: ");
                    var sp = Console.ReadLine() ?? "";
                    var d = _doctorRepo.AddAsync(new Doctor { Name = dn, Speciality = sp })
                                       .GetAwaiter().GetResult();
                    doctorId = d.Id;
                }
                else if (role == UserRole.Patient)
                {
                    Console.Write("\nFull Name: ");
                    var pn = Console.ReadLine() ?? "";
                    Console.Write("Age: ");
                    int.TryParse(Console.ReadLine(), out var age);
                    Console.Write("Address: ");
                    var addr = Console.ReadLine() ?? "";
                    Console.Write("Phone: ");
                    var ph = Console.ReadLine() ?? "";
                    Console.Write("Email: ");
                    var em = Console.ReadLine() ?? "";

                    var p = _patientRepo.AddPatient(new Models.Patient
                    {
                        Name = pn, Age = age, Address = addr, phone = ph, email = em
                    }).GetAwaiter().GetResult();
                    patientId = p.Id;
                }

                _users.RegisterAsync(uname, pwd, role, doctorId, patientId).GetAwaiter().GetResult();
                Console.WriteLine("\nRegistered ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nRegistration failed: {ex.Message}");
            }
            Pause();
        }

        private bool Login()
        {
            Console.Clear();
            HeaderHelper.DrawHeader("User Login");
            Console.Write("Username: ");
            var uname = Console.ReadLine() ?? "";
            Console.Write("Password: ");
            var pwd = ReadPassword();

            var ok = _users.VerifyAsync(uname, pwd).GetAwaiter().GetResult();
            if (!ok)
            {
                Console.WriteLine("Invalid credentials ");
                Pause();
                return true;
            }

            var user = _users.GetByUsernameAsync(uname).GetAwaiter().GetResult()!;
            _currentUser = user.Username;
            _currentRole = user.Role;
            _currentDoctorId = user.DoctorId;
            _currentPatientId = user.PatientId;

            Console.WriteLine($"Login successful  (Role: {_currentRole})");
            Pause();
            return true;
        }

        private static string ReadPassword()
        {
            var pass = "";
            ConsoleKeyInfo key;
            while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                { pass = pass[..^1]; Console.Write("\b \b"); }
                else if (!char.IsControl(key.KeyChar))
                { pass += key.KeyChar; Console.Write("*"); }
            }
            Console.WriteLine();
            return pass;
        }

        private static void Pause()
        {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }
}
