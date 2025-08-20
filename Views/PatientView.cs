using System;
using HospitalManagementApplication.Models;
using HospitalManagementApplication.Controllers;
using HospitalManagementApplication.ViewModels;



namespace HospitalManagementApplication.Views
{
    public class PatientView
    {
        private readonly PatientController _controller;

        public PatientView(PatientController controller)
        {
            _controller = controller;
        }
    
       public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Manage Patients ---");
                Console.WriteLine("1. Add Patient");
                Console.WriteLine("2. View All Patients");
                Console.WriteLine("3. Search Patient by Name");
                Console.WriteLine("4. Edit Patient");
                Console.WriteLine("5. Delete Patient");
                Console.WriteLine("6. Exit");
                Console.Write("Enter choice: ");
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
                        SearchByName();
                        break;
                    case "4":
                        EditPatient();
                        break;
                    case "5":
                        DeletePatient();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
        // public void ListOfDoctors(){
        //     var doctors = _viewModel.GetAllPatients();
        //     foreach (var p in doctors)
        //     {
        //         Console.WriteLine("|---------------------------------------------------|");
        //         Console.WriteLine($"|ID: {p.Id}| Name: {p.Name}| Age: {p.Age}| Disease: {p.Disease}|");
        //         Console.WriteLine("|---------------------------------------------------|");
        //     }
        // }

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

            var vm = new PatientCreateVM { Name = name, Age = age, Disease = disease };
            var result = _controller.Create(vm);
            Console.WriteLine($"Patient Added: ID = {result.Id}, Name = {result.Name}");
        }
        private void SearchByName(){
            Console.Write("Enter Patient Name or Surname: ");
            string name = Console.ReadLine()!;
            foreach(var p in _controller.SearchByName(name)){
                Console.WriteLine($"Patient Found: ID = {p.Id}, Name = {p.Name}");
            }
            
        }

        private void EditPatient(){
            Console.Write("Enter the Patient Id");
            int id = int.Parse(Console.ReadLine()!);
            var vm = new PatientEditVM{
                Id = id
                            };
            vm = _controller.GetForEdit(vm.Id); 
            if(vm.Id==null){
                Console.Write("Patient Not Found!!");
            }
            else{
                Console.Write("Enter the Patient Name");
                vm.Name = Console.ReadLine()!;
                Console.Write("Enter the Patient Age");
                vm.Age = int.Parse(Console.ReadLine()!);
                Console.Write("Enter the Patient Disease");
                vm.Disease = Console.ReadLine()!;
                Console.Write("Enter the Patient Primary Doctor Id");
                vm.PrimaryDoctorId = int.Parse(Console.ReadLine()!);
                _controller.Update(vm);
            }

        }
        private void DeletePatient(){
            Console.Write("Enter Patient to be Deleted");
            int id = int.Parse(Console.ReadLine()!);
            
            string confirm = Console.ReadLine()!;
            if(confirm == "y"){
                _controller.Delete(id);
            }
            else{
                Console.WriteLine("Cancelled!!");
            }

        }
        private void ViewAllPatients()
        {
            var patients = _controller.GetAllPatients();
             Console.WriteLine("{0,-6} {1,-20} {2,-5} {3}", "ID", "Name", "Age", "Disease");
            Console.WriteLine(new string('-', 60));
            foreach (var p in patients)
            {
               
                Console.WriteLine("{0,-6} {1,-20} {2,-5} {3}", p.Id, p.Name, p.Age, p.Disease);
            }
        }
    }
}
