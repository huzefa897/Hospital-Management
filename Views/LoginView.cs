using System;
using System.Collections.Generic;
using HospitalManagementApplication.Views;
using HospitalManagementApplication.Headers;
using HospitalManagementApplication.Util;
namespace HospitalManagementApplication.Views
{
    public class LoginView  
    {

        private readonly PatientView _patientView;
        private readonly DoctorsView? _doctorsView;

        private static Dictionary<string, string> users = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private static string? currentUser = null;

        public LoginView(PatientView patientView, DoctorsView? doctorsView = null)
        {
            _patientView = patientView;
            _doctorsView = doctorsView;
        }

        public void Run()
        {
            while (true)
            {
                if (currentUser == null)
                {
                    ShowLoginPage();
                }
                else
                {
                    ShowHome();
                }
            }
        }

        private void ShowHome()
        {
            var homeView = new HomeView(_patientView, _doctorsView, currentUser);
            homeView.Run();
        }

        private static void ShowLoginPage()
        {
            Console.Clear();
            HeaderHelper.DrawHeader("User Options");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("Q. Exit");
            Console.Write("\nSelect: ");

            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.D1) Register();
            else if (key == ConsoleKey.D2) Login();
            else if (key == ConsoleKey.Q) Environment.Exit(0);
        }

        private static void Register()
        {
            Console.Clear();
            HeaderHelper.DrawHeader("User Registration");
            Console.Write("Enter Your Username: ");
            string username = Console.ReadLine() ?? "";
            Console.Write("Enter Your Password: ");
            string password = ReadPassword();

            if (users.ContainsKey(username))
            {
                Console.WriteLine("User Already Exists");
                Pause(Register);
            }
            else
            {
                users[username] = password;
                Console.WriteLine("User registered successfully!");
                Pause(Login);
            }
        }

        private static void Login()
        {
            Console.Clear();
            HeaderHelper.DrawHeader("User Login");
            Console.Write("Enter Your Username: ");
            string username = Console.ReadLine() ?? "";
            Console.Write("Enter Your Password: ");
            string password = ReadPassword();

            if (users.TryGetValue(username, out string? storedPassword) && storedPassword == password)
            {
                currentUser = username;
                Console.WriteLine("Login successful!");
                Pause(() => { }); // go back to main loop, which shows home
            }
            else
            {
                Console.WriteLine("Invalid username or password");
                Pause(Login);
            }
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

        private static void Pause(Action nextAction)
        {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey(true);
            nextAction();
        }
    }
}
