using System;
using HospitalManagementApplication.Models;
using HospitalManagementApplication.ViewModels;

namespace HospitalManagementApplication.Views
{
    public class DoctorsView
    {

        public void Show()
        {
            Console.WriteLine("Doctor view placeholder.");
        }
//         private readonly DoctorViewModel _viewModel;

//         public DoctorsView(DoctorViewModel viewModel)
//         {
//             _viewModel = viewModel;
//         }
//     public void DoctorView(){
//         while(true){
// Console.WriteLine("Hospital Management Application");
// Console.WriteLine("Choose the Following Options");
// Console.WriteLine("1.Manage Patients");
// Console.WriteLine("2.List the Doctors");
// Console.WriteLine("3.Exit");
// Console.Write("Enter your choice: ");
// var choice = Console.ReadLine();
// switch (choice)
// {
//     case "1":
//         ShowMenu();
//         break;
//     case "2":
//         ListOfDoctors();
//         break;
//     case "3":
//         break;
//     default:
//         Console.WriteLine("Invalid choice.");
//         break;
// }            
//         }
//     }
//         public void ShowMenu()
//         {
//             while (true)
//             {
//                 Console.WriteLine("\n--- Doctor View ---");
//                 Console.WriteLine("1. List of Doctors");
//                 Console.WriteLine("2. Exit");

//                 Console.Write("Enter your choice: ");
//                 var choice = Console.ReadLine();

//                 switch (choice)
//                 {
//                     case "1":
//                         ListOfDoctors();
//                         break;
//                     case "2":
//                         return;
//                     default:
//                         Console.WriteLine("Invalid choice.");
//                         break;
//                 }
//             }
//         }
//         public void ListOfDoctors(){    
//             // var doctors = _viewModel.GetAllDoctors();
//             // foreach (var d in doctors)
//             // {
//             //     Console.WriteLine("|---------------------------------------------------|");
//             //     Console.WriteLine($"|ID: {d.Id}| Name: {d.Name}| Age: {d.Age}| Disease: {d.Disease}|");
//             //     Console.WriteLine("|---------------------------------------------------|");
//             // }
//         }

    }
}
