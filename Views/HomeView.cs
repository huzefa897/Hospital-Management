using System;
using HospitalManagementApplication.Headers;
using HospitalManagementApplication.Models;

namespace HospitalManagementApplication.Views
{
    public class HomeView
    {
        private readonly PatientView _patientView;
        private readonly DoctorsView _doctorsView;
        private readonly AppointmentView _apptView;
        private readonly string _currentUser;
        private readonly UserRole _role;
        private readonly int? _doctorId;
        private readonly int? _patientId;

        public HomeView(PatientView patientView, DoctorsView doctorsView, AppointmentView apptView,
                        string currentUser, UserRole role, int? doctorId, int? patientId)
        {
            _patientView = patientView;
            _doctorsView = doctorsView;
            _apptView = apptView;
            _currentUser = currentUser;
            _role = role;
            _doctorId = doctorId;
            _patientId = patientId;
        }

        // returns true = logout, false = quit app
        public bool Run()
        {
            while (true)
            {
                Console.Clear();
                HeaderHelper.DrawHeader($"Home — {_currentUser} ({_role})");

                switch (_role)
                {
                    case UserRole.Admin:
                        Console.WriteLine("1) Manage Patients");
                        Console.WriteLine("2) Manage Doctors");
                        Console.WriteLine("3) Appointments (Admin)");
                        Console.WriteLine("L) Logout");
                        Console.WriteLine("Q) Quit");
                        Console.Write("\nSelect: ");
                        var ak = Console.ReadKey(true).Key;
                        if (ak == ConsoleKey.D1) _patientView.ShowMenu();
                        else if (ak == ConsoleKey.D2) _doctorsView.Run();
                        else if (ak == ConsoleKey.D3) _apptView.AdminMenu();
                        else if (ak == ConsoleKey.L) return true;
                        else if (ak == ConsoleKey.Q) return false;
                        break;

                    case UserRole.Doctor:
                        Console.WriteLine("1) My Appointments");
                        Console.WriteLine("2) Manage Patients");
                        Console.WriteLine("L) Logout");
                        Console.WriteLine("Q) Quit");
                        Console.Write("\nSelect: ");
                        var dk = Console.ReadKey(true).Key;
                        if (dk == ConsoleKey.D1) _apptView.DoctorMenu(_doctorId ?? 0);
                        else if (dk == ConsoleKey.D2) _patientView.ShowMenu();
                        else if (dk == ConsoleKey.L) return true;
                        else if (dk == ConsoleKey.Q) return false;
                        break;

                    default: // Patient
                        Console.WriteLine("1) Book Appointment");
                        Console.WriteLine("2) My Appointments");
                        Console.WriteLine("3) Manage My Profile");
                        Console.WriteLine("L) Logout");
                        Console.WriteLine("Q) Quit");
                        Console.Write("\nSelect: ");
                        var pk = Console.ReadKey(true).Key;
                        if (pk == ConsoleKey.D1) _apptView.PatientMenu(_patientId ?? 0);
                        else if (pk == ConsoleKey.D2) _apptView.PatientMenu(_patientId ?? 0);
                        else if (pk == ConsoleKey.D3) _patientView.ShowMenu();
                        else if (pk == ConsoleKey.L) return true;
                        else if (pk == ConsoleKey.Q) return false;
                        break;
                }
            }
        }
    }
}
