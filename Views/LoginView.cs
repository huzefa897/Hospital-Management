using System;
using System.Collections.Generic;
using HospitalManagementApplication.Views;
using HospitalManagementApplication.Headers;


namespace HospitalManagementApplication.Views
{
    public class LoginView
    {
        private readonly PatientView _patientView;
        private readonly DoctorsView? _doctorsView;

        private static readonly Dictionary<string, string> users =
            new(StringComparer.OrdinalIgnoreCase);

        private static string? currentUser = null;

        public LoginView(PatientView patientView, DoctorsView? doctorsView = null)
        {
            _patientView = patientView;
            _doctorsView = doctorsView;
        }

        // Returns true to keep app running, false to exit program
        public bool Run()
        {
            while (true)
            {
                if (currentUser == null)
                {
                    bool stayInApp = ShowLoginPage();
                    if (!stayInApp) return false; // user chose Exit
                }
                else
                {
                    // Go to home, and handle their result:
                    // assume HomeView.Run() => true = logged out, false = quit
                    var homeView = new HomeView(_patientView, _doctorsView, currentUser!);
                    bool loggedOut = homeView.Run();
                    if (loggedOut)
                    {
                        currentUser = null; // back to login loop
                    }
                    else
                    {
                        return false; // quit app
                    }
                }
            }
        }

        // Shows login menu. Returns true to continue app, false to quit.
        private static bool ShowLoginPage()
        {
            Console.Clear();
            HeaderHelper.DrawHeader("User Options");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("Q. Exit");
            Console.Write("\nSelect: ");

            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.D1)
            {
                Register();
                return true; // return to login menu
            }
            else if (key == ConsoleKey.D2)
            {
                bool ok = Login();
                return ok; // if login succeeded, caller will go to Home
            }
            else if (key == ConsoleKey.Q)
            {
                return false; // signal Program to exit
            }

            // Any other key: just continue showing the login menu
            return true;
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
                Pause(() => { }); // just return to login menu
            }
            else
            {
                users[username] = password;
                Console.WriteLine("User registered successfully!");
                // If you want to immediately try login after register:
                // Pause(() => { Login(); });
                Pause(() => { }); // or just pause and return to menu
            }
        }

        // Returns true if login succeeded (so caller can go to Home)
        private static bool Login()
        {
            Console.Clear();
            HeaderHelper.DrawHeader("User Login");
            Console.Write("Enter Your Username: ");
            string username = Console.ReadLine() ?? "";
            Console.Write("Enter Your Password: ");
            string password = ReadPassword();

            if (users.TryGetValue(username, out string? storedPassword) &&
                storedPassword == password)
            {
                currentUser = username;
                Console.WriteLine("Login successful!");
                Pause(() => { });
                return true;
            }
            else
            {
                Console.WriteLine("Invalid username or password");
                // IMPORTANT: Login returns bool, but Pause wants Action
                Pause(() => { }); // don’t pass Login directly
                return false;
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
