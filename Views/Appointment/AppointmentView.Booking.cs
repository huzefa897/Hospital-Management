using System;

namespace HospitalManagementApplication.Views
{
    public partial class AppointmentView
    {
        private (DateTime? startUtc, string reason) AskWhenAndWhy()
        {
            Console.Write("\nWhen? (dd-MM-yy HH:mm): ");
            var txt = (Console.ReadLine() ?? "").Trim();

            if (!TryParseLocal(txt, out var local))
            {
                Console.WriteLine("Invalid date/time. Example: 25-09-25 14:30");
                return (null, "");
            }
            var startUtc = DateTime.SpecifyKind(local, DateTimeKind.Local).ToUniversalTime();

            Console.Write("Reason: ");
            var reason = (Console.ReadLine() ?? "").Trim();
            return (startUtc, reason);
        }

        private void TryBook(int patientId, int doctorId, DateTime startUtc, string reason)
        {
            try
            {
                var appt = _appts.BookAsync(patientId, doctorId, startUtc, 30, reason).GetAwaiter().GetResult();
                var local = DateTime.SpecifyKind(appt.StartUtc, DateTimeKind.Utc).ToLocalTime();
                Console.WriteLine($"\nBooked ✅  #{appt.Id} on {local:dd-MM-yy HH:mm} with Doctor:{doctorId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFailed: {ex.Message}");
            }
            Pause();
        }
    }
}
