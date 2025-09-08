using System;
using HospitalManagementApplication.Models;
using HospitalManagementApplication.ViewModels;
using HospitalManagementApplication.Views;
using HospitalManagementApplication.Headers;

namespace HospitalManagementApplication.Views
{
    public class HomeView
    {
        private readonly PatientView _patientView;
        private readonly DoctorsView _doctorsView;
        private readonly string _user;
        public HomeView(PatientView patientView, DoctorsView doctorsView, string currentUser)
        {
            _patientView = patientView;
            _doctorsView = doctorsView;
            _user = currentUser;
        }

        public bool Run()
        {
            while (true)
            {
               HeaderHelper.DrawHeader("Home");
                Console.WriteLine("Hello " + _user);
                Console.WriteLine("1. Manage Patients");
                Console.WriteLine("2. List the Doctors");
                Console.WriteLine("3. Logout");
                Console.WriteLine("Q: Quit");
                Console.WriteLine("----------------------------------------");
                Console.Write("Enter your choice (1-3): ");

               // var choice = Console.ReadLine();
                Console.WriteLine(); // spacing
                var key = Console.ReadKey(true).Key;
            
                switch (key)
                {
                    case ConsoleKey.D1:
                        _patientView.ShowMenu();
                        break;

                    case ConsoleKey.D2:
                        if (_doctorsView != null)
                        {
                            _doctorsView.Show();
                        }
                        else
                        {
                            Console.WriteLine("⚠️ Doctors section is not implemented yet.");
                            Console.WriteLine("Press Enter to continue...");
                            Console.ReadLine();
                        }
                        break;


                       case ConsoleKey.D3:
                            // Console.WriteLine("Thank you for using the Hospital Management System.");
                            return true;
                            break;

                        case ConsoleKey.Q:
                            Console.WriteLine("Thank you for using the Hospital Management System.");
                            Environment.Exit(0); 
                            break;

                    default:
                        Console.WriteLine("❌ Invalid choice. Please enter a number between 1 and 3.");
                        Console.WriteLine("Press Enter to try again...");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
