using System;
using System.Linq;
using System.Collections.Generic;
using HospitalManagementApplication.Headers;
using HospitalManagementApplication.Models;

namespace HospitalManagementApplication.Views
{
    public partial class AppointmentView
    {
        private int? SelectDoctorId()
        {
            Console.Clear();
            HeaderHelper.DrawHeader("Select Doctor");
            var docs = _doctors.GetAllAsync().GetAwaiter().GetResult();
            if (!docs.Any()) { Console.WriteLine("(no doctors)"); Pause(); return null; }

            PrintDoctorsTable(docs);
            return AskInt("\nDoctor Id: ");
        }

        private int? SelectPatientId()
        {
            Console.Clear();
            HeaderHelper.DrawHeader("Select Patient");
            var pts = _patients.GetAll().GetAwaiter().GetResult().ToList();
            if (!pts.Any()) { Console.WriteLine("(no patients)"); Pause(); return null; }

            PrintPatientsTable(pts);
            return AskInt("\nPatient Id: ");
        }

        private void PrintAppointmentsTable(IEnumerable<Appointment> appts, bool includePatient, bool includeDoctor)
        {
            var list = appts.OrderBy(a => a.StartUtc).ToList();
            if (!list.Any()) { Console.WriteLine("(none)"); return; }

            var cols = new List<(string title, int width)>
            {
                ("Id", 5),
                ("Date", 10),
                ("Time", 5),
                ("Dur", 4),
                ("Status", 9),
                ("Reason", 24)
            };
            if (includePatient) cols.Insert(1, ("Patient", 8));
            if (includeDoctor)  cols.Insert(1, ("Doctor", 7));

            WriteRow(cols.Select(c => c.title).ToArray(), cols.Select(c => c.width).ToArray(), isHeader: true);

            foreach (var a in list)
            {
                var local = DateTime.SpecifyKind(a.StartUtc, DateTimeKind.Utc).ToLocalTime();
                var cells = new List<string> { a.Id.ToString() };

                if (includePatient) cells.Add(a.PatientId.ToString());
                if (includeDoctor)  cells.Add(a.DoctorId.ToString());

                cells.Add(local.ToString("dd-MM-yy"));
                cells.Add(local.ToString("HH:mm"));
                cells.Add(a.DurationMinutes.ToString());
                cells.Add(a.Status.ToString());
                cells.Add(a.Reason);

                WriteRow(cells.ToArray(), cols.Select(c => c.width).ToArray(), isHeader: false);
            }
        }

        private static void PrintDoctorsTable(IEnumerable<Doctor> docs)
        {
            var list = docs.OrderBy(d => d.Name).ToList();
            WriteRow(new[] { "Id", "Name", "Speciality" }, new[] { 5, 22, 22 }, isHeader: true);
            foreach (var d in list)
                WriteRow(new[] { d.Id.ToString(), d.Name, d.Speciality ?? "" }, new[] { 5, 22, 22 }, false);
        }

        private static void PrintPatientsTable(IEnumerable<Patient> pts)
        {
            var list = pts.OrderBy(p => p.Name).ToList();
            WriteRow(new[] { "Id", "Name", "Age", "Phone", "Email" }, new[] { 5, 22, 4, 14, 26 }, true);
            foreach (var p in list)
                WriteRow(new[] { p.Id.ToString(), p.Name, p.Age.ToString(), p.phone ?? "", p.email ?? "" }, new[] { 5, 22, 4, 14, 26 }, false);
        }
    }
}
