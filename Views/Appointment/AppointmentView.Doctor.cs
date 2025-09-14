using System;
using HospitalManagementApplication.Headers;

namespace HospitalManagementApplication.Views
{
    public partial class AppointmentView
    {
        // -------- Doctor menu --------
        public void DoctorMenu(int doctorId)
        {
            while (true)
            {
                Console.Clear();
                HeaderHelper.DrawHeader("Appointments — Doctor");
                Console.WriteLine("1) My Appointments");
                Console.WriteLine("2) Book (for a Patient)");
                Console.WriteLine("3) Complete");
                Console.WriteLine("4) Cancel");
                Console.WriteLine("Q) Back");
                Console.Write("\nSelect: ");
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1: ListForDoctor(doctorId); break;
                    case ConsoleKey.D2: BookForDoctor(doctorId); break;
                    case ConsoleKey.D3: Complete(); break;
                    case ConsoleKey.D4: Cancel(); break;
                    case ConsoleKey.Q: return;
                }
            }
        }

        private void BookForDoctor(int doctorId)
        {
            var pid = SelectPatientId();
            if (pid == null) return;

            var (startUtc, reason) = AskWhenAndWhy();
            if (startUtc == null) return;

            TryBook(pid.Value, doctorId, startUtc.Value, reason);
        }

        public void ListForDoctor(int doctorId)
        {
            Console.Clear();
            HeaderHelper.DrawHeader($"Doctor #{doctorId} — Appointments");
            var list = _appts.ForDoctorAsync(doctorId).GetAwaiter().GetResult();
            PrintAppointmentsTable(list, includePatient: true, includeDoctor: false);
            Pause();
        }

        private void Complete()
        {
            var id = AskInt("Appointment Id: ");
            if (id == null) return;
            var ok = _appts.CompleteAsync(id.Value).GetAwaiter().GetResult();
            Console.WriteLine(ok ? "Completed " : "Not found ");
            Pause();
        }

        private void Cancel()
        {
            var id = AskInt("Appointment Id: ");
            if (id == null) return;
            var ok = _appts.CancelAsync(id.Value).GetAwaiter().GetResult();
            Console.WriteLine(ok ? "Cancelled ✅" : "Not found ❌");
            Pause();
        }
    }
}
