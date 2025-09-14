using System;
using System.Linq;
using HospitalManagementApplication.Headers;

namespace HospitalManagementApplication.Views
{
    public partial class AppointmentView
    {
        // -------- Patient menu --------
        public void PatientMenu(int patientId)
        {
            while (true)
            {
                Console.Clear();
                HeaderHelper.DrawHeader("Appointments — Patient");
                Console.WriteLine("1) Book");
                Console.WriteLine("2) My Appointments");
                Console.WriteLine("3) Cancel");
                Console.WriteLine("Q) Back");
                Console.Write("\nSelect: ");
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1: BookForPatient(patientId); break;
                    case ConsoleKey.D2: ListForPatient(patientId); break;
                    case ConsoleKey.D3: Cancel(); break;
                    case ConsoleKey.Q: return;
                }
            }
        }

        private void BookForPatient(int patientId)
        {
            var did = SelectDoctorId();
            if (did == null) return;

            var (startUtc, reason) = AskWhenAndWhy();
            if (startUtc == null) return;

            TryBook(patientId, did.Value, startUtc.Value, reason);
        }

        public void ListForPatient(int patientId)
        {
            Console.Clear();
            HeaderHelper.DrawHeader($"Patient #{patientId} — My Appointments");
            var list = _appts.ForPatientAsync(patientId).GetAwaiter().GetResult();
            PrintAppointmentsTable(list, includePatient: false, includeDoctor: true);
            Pause();
        }
    }
}
