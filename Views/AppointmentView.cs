// using System;
// using System.Globalization;
// using System.Linq;
// using System.Collections.Generic;
// using HospitalManagementApplication.Headers;
// using HospitalManagementApplication.Interfaces;
// using HospitalManagementApplication.Models;

// namespace HospitalManagementApplication.Views
// {
//     public class AppointmentView
//     {
//         private readonly IAppointmentRepository _appts;
//         private readonly IDoctorRepository _doctors;
//         private readonly IPatientRepository _patients;

//         private static readonly string[] INPUT_PATTERNS = new[]
//         {
//             "dd-MM-yy HH:mm",      // requested format
//             "dd-MM-yyyy HH:mm"     // fallback if users type full year
//         };

//         public AppointmentView(IAppointmentRepository appts, IDoctorRepository doctors, IPatientRepository patients)
//         {
//             _appts = appts;
//             _doctors = doctors;
//             _patients = patients;
//         }

//         // -------- Patient menu --------
//         public void PatientMenu(int patientId)
//         {
//             while (true)
//             {
//                 Console.Clear();
//                 HeaderHelper.DrawHeader("Appointments — Patient");
//                 Console.WriteLine("1) Book");
//                 Console.WriteLine("2) My Appointments");
//                 Console.WriteLine("3) Cancel");
//                 Console.WriteLine("Q) Back");
//                 Console.Write("\nSelect: ");
//                 var key = Console.ReadKey(true).Key;

//                 switch (key)
//                 {
//                     case ConsoleKey.D1: BookForPatient(patientId); break;
//                     case ConsoleKey.D2: ListForPatient(patientId); break;
//                     case ConsoleKey.D3: Cancel(); break;
//                     case ConsoleKey.Q: return;
//                 }
//             }
//         }

//         // -------- Doctor menu --------
//         public void DoctorMenu(int doctorId)
//         {
//             while (true)
//             {
//                 Console.Clear();
//                 HeaderHelper.DrawHeader("Appointments — Doctor");
//                 Console.WriteLine("1) My Appointments");
//                 Console.WriteLine("2) Book (for a Patient)");
//                 Console.WriteLine("3) Complete");
//                 Console.WriteLine("4) Cancel");
//                 Console.WriteLine("Q) Back");
//                 Console.Write("\nSelect: ");
//                 var key = Console.ReadKey(true).Key;

//                 switch (key)
//                 {
//                     case ConsoleKey.D1: ListForDoctor(doctorId); break;
//                     case ConsoleKey.D2: BookForDoctor(doctorId); break;
//                     case ConsoleKey.D3: Complete(); break;
//                     case ConsoleKey.D4: Cancel(); break;
//                     case ConsoleKey.Q: return;
//                 }
//             }
//         }

//         // -------- Admin menu --------
//         public void AdminMenu()
// {
//     while (true)
//     {
//         Console.Clear();
//         HeaderHelper.DrawHeader("Appointments — Admin");
//         Console.WriteLine("1) Book (choose Patient & Doctor)");
//         Console.WriteLine("2) List ALL appointments");            // NEW
//         Console.WriteLine("3) List by Doctor");
//         Console.WriteLine("4) List by Patient");
//         Console.WriteLine("5) Complete");
//         Console.WriteLine("6) Cancel");
//         Console.WriteLine("Q) Back");
//         Console.Write("\nSelect: ");
//         var key = Console.ReadKey(true).Key;

//         switch (key)
//         {
//             case ConsoleKey.D1: BookAsAdmin(); break;
//             case ConsoleKey.D2: ListAll(); break;                  // NEW
//             case ConsoleKey.D3:
//                 var did = SelectDoctorId(); if (did != null) ListForDoctor(did.Value);
//                 break;
//             case ConsoleKey.D4:
//                 var pid = SelectPatientId(); if (pid != null) ListForPatient(pid.Value);
//                 break;
//             case ConsoleKey.D5: Complete(); break;
//             case ConsoleKey.D6: Cancel(); break;
//             case ConsoleKey.Q:  return;
//         }
//     }
// }

//         // -------- Booking flows --------
//         private void BookForPatient(int patientId)
//         {
//             var did = SelectDoctorId();
//             if (did == null) return;

//             var (startUtc, reason) = AskWhenAndWhy();
//             if (startUtc == null) return;

//             TryBook(patientId, did.Value, startUtc.Value, reason);
//         }

//         private void BookForDoctor(int doctorId)
//         {
//             var pid = SelectPatientId();
//             if (pid == null) return;

//             var (startUtc, reason) = AskWhenAndWhy();
//             if (startUtc == null) return;

//             TryBook(pid.Value, doctorId, startUtc.Value, reason);
//         }

//         private void BookAsAdmin()
//         {
//             var pid = SelectPatientId(); if (pid == null) return;
//             var did = SelectDoctorId();  if (did == null) return;

//             var (startUtc, reason) = AskWhenAndWhy();
//             if (startUtc == null) return;

//             TryBook(pid.Value, did.Value, startUtc.Value, reason);
//         }

//         private void TryBook(int patientId, int doctorId, DateTime startUtc, string reason)
//         {
//             try
//             {
//                 var appt = _appts.BookAsync(patientId, doctorId, startUtc, 30, reason).GetAwaiter().GetResult();
//                 var local = DateTime.SpecifyKind(appt.StartUtc, DateTimeKind.Utc).ToLocalTime();
//                 Console.WriteLine($"\nBooked ✅  #{appt.Id} on {local:dd-MM-yy HH:mm} with Doctor:{doctorId}");
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"\nFailed: {ex.Message}");
//             }
//             Pause();
//         }

//         // -------- Lists (pretty) --------
//         public void ListForDoctor(int doctorId)
//         {
//             Console.Clear();
//             HeaderHelper.DrawHeader($"Doctor #{doctorId} — Appointments");
//             var list = _appts.ForDoctorAsync(doctorId).GetAwaiter().GetResult();
//             PrintAppointmentsTable(list, includePatient: true, includeDoctor: false);
//             Pause();
//         }

//         public void ListForPatient(int patientId)
//         {
//             Console.Clear();
//             HeaderHelper.DrawHeader($"Patient #{patientId} — My Appointments");
//             var list = _appts.ForPatientAsync(patientId).GetAwaiter().GetResult();
//             PrintAppointmentsTable(list, includePatient: false, includeDoctor: true);
//             Pause();
//         }

//         // -------- Status updates --------
//         private void Complete()
//         {
//             var id = AskInt("Appointment Id: ");
//             if (id == null) return;
//             var ok = _appts.CompleteAsync(id.Value).GetAwaiter().GetResult();
//             Console.WriteLine(ok ? "Completed ✅" : "Not found ❌");
//             Pause();
//         }

//         private void Cancel()
//         {
//             var id = AskInt("Appointment Id: ");
//             if (id == null) return;
//             var ok = _appts.CancelAsync(id.Value).GetAwaiter().GetResult();
//             Console.WriteLine(ok ? "Cancelled ✅" : "Not found ❌");
//             Pause();
//         }

//         // -------- Helpers --------
//         private (DateTime? startUtc, string reason) AskWhenAndWhy()
//         {
//             Console.Write("\nWhen? (dd-MM-yy HH:mm): ");
//             var txt = (Console.ReadLine() ?? "").Trim();

//             if (!TryParseLocal(txt, out var local))
//             {
//                 Console.WriteLine("Invalid date/time. Example: 25-09-25 14:30");
//                 return (null, "");
//             }
//             var startUtc = DateTime.SpecifyKind(local, DateTimeKind.Local).ToUniversalTime();

//             Console.Write("Reason: ");
//             var reason = (Console.ReadLine() ?? "").Trim();
//             return (startUtc, reason);
//         }

//         private static bool TryParseLocal(string input, out DateTime local)
//         {
//             return DateTime.TryParseExact(
//                 input,
//                 INPUT_PATTERNS,
//                 CultureInfo.InvariantCulture,
//                 DateTimeStyles.AssumeLocal,
//                 out local
//             );
//         }
//         private void ListAll()
// {
//     Console.Clear();
//     HeaderHelper.DrawHeader("All Appointments");
//     var list = _appts.AllAsync().GetAwaiter().GetResult();
//     // show both Doctor and Patient columns
//     PrintAppointmentsTable(list, includePatient: true, includeDoctor: true);
//     Pause();
// }

//         private int? SelectDoctorId()
//         {
//             Console.Clear();
//             HeaderHelper.DrawHeader("Select Doctor");
//             var docs = _doctors.GetAllAsync().GetAwaiter().GetResult();
//             if (!docs.Any()) { Console.WriteLine("(no doctors)"); Pause(); return null; }

//             PrintDoctorsTable(docs);
//             return AskInt("\nDoctor Id: ");
//         }

//         private int? SelectPatientId()
//         {
//             Console.Clear();
//             HeaderHelper.DrawHeader("Select Patient");
//             var pts = _patients.GetAll().GetAwaiter().GetResult().ToList();
//             if (!pts.Any()) { Console.WriteLine("(no patients)"); Pause(); return null; }

//             PrintPatientsTable(pts);
//             return AskInt("\nPatient Id: ");
//         }

//         private static int? AskInt(string prompt)
//         {
//             Console.Write(prompt);
//             var s = Console.ReadLine();
//             return int.TryParse(s, out var v) ? v : (int?)null;
//         }

//         // -------- Pretty tables --------
//         private void PrintAppointmentsTable(IEnumerable<Appointment> appts, bool includePatient, bool includeDoctor)
//         {
//             var list = appts.OrderBy(a => a.StartUtc).ToList();
//             if (!list.Any()) { Console.WriteLine("(none)"); return; }

//             // Headers
//             var cols = new List<(string title, int width)>
//             {
//                 ("Id", 5),
//                 ("Date", 10),      // dd-MM-yy
//                 ("Time", 5),       // HH:mm
//                 ("Dur", 4),
//                 ("Status", 9),
//                 ("Reason", 24)
//             };
//             if (includePatient) cols.Insert(1, ("Patient", 8));
//             if (includeDoctor)  cols.Insert(1, ("Doctor", 7));

//             WriteRow(cols.Select(c => c.title).ToArray(), cols.Select(c => c.width).ToArray(), isHeader: true);

//             foreach (var a in list)
//             {
//                 var local = DateTime.SpecifyKind(a.StartUtc, DateTimeKind.Utc).ToLocalTime();
//                 var cells = new List<string> { a.Id.ToString() };

//                 if (includePatient) cells.Add(a.PatientId.ToString());
//                 if (includeDoctor)  cells.Add(a.DoctorId.ToString());

//                 cells.Add(local.ToString("dd-MM-yy"));
//                 cells.Add(local.ToString("HH:mm"));
//                 cells.Add(a.DurationMinutes.ToString());
//                 cells.Add(a.Status.ToString());
//                 cells.Add(a.Reason);

//                 WriteRow(cells.ToArray(), cols.Select(c => c.width).ToArray(), isHeader: false);
//             }
//         }

//         private static void PrintDoctorsTable(IEnumerable<Doctor> docs)
//         {
//             var list = docs.OrderBy(d => d.Name).ToList();
//             WriteRow(new[] { "Id", "Name", "Speciality" }, new[] { 5, 22, 22 }, isHeader: true);
//             foreach (var d in list)
//                 WriteRow(new[] { d.Id.ToString(), d.Name, d.Speciality ?? "" }, new[] { 5, 22, 22 }, false);
//         }

//         private static void PrintPatientsTable(IEnumerable<Patient> pts)
//         {
//             var list = pts.OrderBy(p => p.Name).ToList();
//             WriteRow(new[] { "Id", "Name", "Age", "Phone", "Email" }, new[] { 5, 22, 4, 14, 26 }, true);
//             foreach (var p in list)
//                 WriteRow(new[] { p.Id.ToString(), p.Name, p.Age.ToString(), p.phone ?? "", p.email ?? "" }, new[] { 5, 22, 4, 14, 26 }, false);
//         }

//         private static void WriteRow(string[] cells, int[] widths, bool isHeader)
//         {
//             for (int i = 0; i < cells.Length; i++)
//             {
//                 var val = cells[i] ?? "";
//                 var w = widths[i];
//                 if (val.Length > w) val = val.Substring(0, Math.Max(0, w - 1)) + "…";
//                 Console.Write(val.PadRight(w));
//                 Console.Write(" ");
//             }
//             Console.WriteLine();
//             if (isHeader)
//             {
//                 for (int i = 0; i < cells.Length; i++)
//                     Console.Write(new string('─', widths[i]) + " ");
//                 Console.WriteLine();
//             }
//         }

//         private static void Pause()
//         {
//             Console.Write("\nPress any key to continue...");
//             Console.ReadKey(true);
//         }
//     }
// }
