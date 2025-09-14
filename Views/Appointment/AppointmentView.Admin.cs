using System;
using HospitalManagementApplication.Headers;

namespace HospitalManagementApplication.Views
{
    public partial class AppointmentView
    {
        // -------- Admin menu --------
        public void AdminMenu()
        {
            while (true)
            {
                Console.Clear();
                HeaderHelper.DrawHeader("Appointments — Admin");
                Console.WriteLine("1) Book (choose Patient & Doctor)");
                Console.WriteLine("2) List ALL appointments");
                Console.WriteLine("3) List by Doctor");
                Console.WriteLine("4) List by Patient");
                Console.WriteLine("5) Complete");
                Console.WriteLine("6) Cancel");
                Console.WriteLine("Q) Back");
                Console.Write("\nSelect: ");
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1: BookAsAdmin(); break;
                    case ConsoleKey.D2: ListAll(); break;
                    case ConsoleKey.D3:
                        { var did = SelectDoctorId(); if (did != null) ListForDoctor(did.Value); }
                        break;
                    case ConsoleKey.D4:
                        { var pid = SelectPatientId(); if (pid != null) ListForPatient(pid.Value); }
                        break;
                    case ConsoleKey.D5: Complete(); break;
                    case ConsoleKey.D6: Cancel(); break;
                    case ConsoleKey.Q:  return;
                }
            }
        }

        private void BookAsAdmin()
        {
            var pid = SelectPatientId(); if (pid == null) return;
            var did = SelectDoctorId();  if (did == null) return;

            var (startUtc, reason) = AskWhenAndWhy();
            if (startUtc == null) return;

            TryBook(pid.Value, did.Value, startUtc.Value, reason);
        }

        private void ListAll()
        {
            Console.Clear();
            HeaderHelper.DrawHeader("All Appointments");
            var list = _appts.AllAsync().GetAwaiter().GetResult();
            PrintAppointmentsTable(list, includePatient: true, includeDoctor: true);
            Pause();
        }
    }
}
