using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using HospitalManagementApplication.Headers;
using HospitalManagementApplication.Interfaces;
using HospitalManagementApplication.Models;

namespace HospitalManagementApplication.Views
{
    public partial class AppointmentView
    {
        private readonly IAppointmentRepository _appts;
        private readonly IDoctorRepository _doctors;
        private readonly IPatientRepository _patients;

        // Input formats (primary + fallback)
        private static readonly string[] INPUT_PATTERNS = { "dd-MM-yy HH:mm", "dd-MM-yyyy HH:mm" };

        public AppointmentView(IAppointmentRepository appts, IDoctorRepository doctors, IPatientRepository patients)
        {
            _appts = appts;
            _doctors = doctors;
            _patients = patients;
        }

        // ---------- Shared helpers ----------
        private static bool TryParseLocal(string input, out DateTime local) =>
            DateTime.TryParseExact(
                input,
                INPUT_PATTERNS,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeLocal,
                out local
            );

        private static int? AskInt(string prompt)
        {
            Console.Write(prompt);
            var s = Console.ReadLine();
            return int.TryParse(s, out var v) ? v : (int?)null;
        }

        private static void WriteRow(string[] cells, int[] widths, bool isHeader)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                var val = cells[i] ?? "";
                var w = widths[i];
                if (val.Length > w) val = val.Substring(0, Math.Max(0, w - 1)) + "…";
                Console.Write(val.PadRight(w));
                Console.Write(" ");
            }
            Console.WriteLine();
            if (isHeader)
            {
                for (int i = 0; i < cells.Length; i++)
                    Console.Write(new string('─', widths[i]) + " ");
                Console.WriteLine();
            }
        }

        private static void Pause()
        {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }
}
