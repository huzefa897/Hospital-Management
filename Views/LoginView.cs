using System;
using HospitalManagementApplication.Headers;
using HospitalManagementApplication.Interfaces;

namespace HospitalManagementApplication.Views
{
    public class LoginView
    {
        private readonly PatientView _patientView;
        private readonly DoctorsView? _doctorsView;
        private readonly IUserRepository _users;   // <- real repo (DB-backed)
        private string? _currentUser;

        public LoginView(PatientView patientView, IUserRepository users, DoctorsView? doctorsView = null)
        {
            _patientView = patientView;
            _doctorsView = doctorsView;
            _users = users;
        }

        // Returns true to keep app running, false to exit program
        public bool Run()
        {
            while (true)
            {
                if (_currentUser == null)
                {
                    bool stayInApp = ShowLoginPage();
                    if (!stayInApp) return false; // user chose Exit
                }
                else
                {
                    var homeView = new HomeView(_patientView, _doctorsView, _currentUser!);
                    bool loggedOut = homeView.Run();
                    if (loggedOut) _currentUser = null;   // back to login menu
                    else return false;                    // quit app
                }
            }
        }

        // Shows login menu. Returns true to continue app, false to quit.
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
        case ConsoleKey.D1:
        case ConsoleKey.NumPad1:
            Register();
            return true;                  

        case ConsoleKey.D2:
        case ConsoleKey.NumPad2:
            return Login();               

        case ConsoleKey.Q:
            return false;                 

        default:
            return true;                  
    }
        }

        private void Register()
        {
            Console.Clear();
            HeaderHelper.DrawHeader("User Registration");
            Console.Write("Enter Your Username: ");
            string username = Console.ReadLine() ?? "";
            Console.Write("Enter Your Password: ");
            string password = ReadPassword();

            try
            {
                // sync wrapper since your views are sync
                _users.RegisterAsync(username, password).GetAwaiter().GetResult();
                Console.WriteLine("User registered successfully! ✅");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Registration failed: {ex.Message}");
            }
            Pause();
        }

        // Returns true if app should continue (login ok or just return to menu),
        // false only if you want to exit entire app from here.
        private bool Login()
        {
            Console.Clear();
            HeaderHelper.DrawHeader("User Login");
            Console.Write("Enter Your Username: ");
            string username = Console.ReadLine() ?? "";
            Console.Write("Enter Your Password: ");
            string password = ReadPassword();

            var ok = _users.VerifyAsync(username, password).GetAwaiter().GetResult();
            if (ok)
            {
                _currentUser = username;
                Console.WriteLine("Login successful! ✅");
                Pause();
                return true; // go to Home
            }

            Console.WriteLine("Invalid username or password ❌");
            Pause();
            return true; // stay in login menu
        }

        private static string ReadPassword()
        {
            var pass = "";
            ConsoleKeyInfo key;
            while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass = pass[..^1];
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
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
