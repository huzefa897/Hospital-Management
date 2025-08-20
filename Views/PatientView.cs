using System;
using HospitalManagementApplication.Models;
using HospitalManagementApplication.ViewModels;

namespace HospitalManagementApplication.Views
{
    public class PatientView
    {
        private readonly PatientViewModel _viewModel;

        public PatientView(PatientViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Manage Patients ---");
                Console.WriteLine("1. Add Patient");
                Console.WriteLine("2. View All Patients");
                Console.WriteLine("3. Exit");

                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddPatient();
                        break;
                    case "2":
                        ViewAllPatients();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        public void AddPatient()
        {
            Console.Write("Enter Patient Name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter Patient Age: ");
            int age;
            while (!int.TryParse(Console.ReadLine(), out age))
            {
                Console.Write("Invalid input. Enter numeric age: ");
            }

            Console.Write("Enter Disease: ");
            string disease = Console.ReadLine()!;

            var patient = new Patient { Name = name, Age = age, Disease = disease };
            _viewModel.AddPatient(patient);
            Console.WriteLine("Patient added successfully!");
        }

        private void ViewAllPatients()
        {
            var patients = _viewModel.GetAllPatients();
            foreach (var p in patients)
            {
                Console.WriteLine($"ID: {p.Id}, Name: {p.Name}, Age: {p.Age}, Disease: {p.Disease}");
            }
        }
    }
}
