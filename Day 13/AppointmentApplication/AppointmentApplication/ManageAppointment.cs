using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentApplication.Interfaces;
using AppointmentApplication.Models;

namespace AppointmentApplication
{
    public class ManageAppointment
    {
        private readonly IAppointmentService _appointmentService;

        public ManageAppointment(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public void Start()
        {
            bool exit = false;
            while (!exit)
            {
                PrintMenu();
                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddAppointment();
                        break;
                    case "2":
                        SearchAppointments();
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }


        private void PrintMenu()
        {
            Console.WriteLine("\n  Appointment Application \n");
            Console.WriteLine("1. Add Appointment");
            Console.WriteLine("2. Search Appointments");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");
        }

        private void AddAppointment()
        {
            var appt = new Appointment();

            Console.Write("Enter Patient Name: ");
            appt.PatientName = Console.ReadLine() ?? "";

            Console.Write("Enter Patient Age: ");
            int age;
            while (!int.TryParse(Console.ReadLine(), out age)) 
                Console.Write("Enter valid age: ");
            appt.PatientAge = age;

            Console.Write("Enter Appointment Date and Time (yyyy-MM-dd HH:mm): ");
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date) || date <= DateTime.Now) 
                Console.Write("Enter valid date/time: ");
            appt.AppointmentDate = date;

            Console.Write("Enter Reason for Visit: ");
            appt.Reason = Console.ReadLine() ?? "";

            int id = _appointmentService.AddAppointment(appt);
            Console.WriteLine($"Appointment added successfully. ID = {id}");
        }

        private void SearchAppointments()
        {
            var model = new AppointmentSearchModel();

            Console.Write("Search by Name (Blank to skip): ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name)) 
                model.PatientName = name;

            Console.Write("Search by Appointment Date? (yyyy-MM-dd or leave blank): ");
            var dateStr = Console.ReadLine();
            if (DateTime.TryParse(dateStr, out DateTime dt)) 
                model.AppointmentDate = dt;

            Console.Write("Search by Age Range? (y/n): ");
            if ((Console.ReadLine() ?? "").ToLower() == "y")
            {
                var range = new Range<int>();
                Console.Write("Min Age: ");
                int minval;
                while (!int.TryParse(Console.ReadLine(), out minval)) 
                    Console.Write("Enter valid number: ");
                range.MinValue = minval;
                int maxval;
                Console.Write("Max Age: ");
                while (!int.TryParse(Console.ReadLine(), out maxval)) 
                    Console.Write("Enter valid number: ");
                range.MaxValue = maxval;
                model.AgeRange = range;
            }

            var results = _appointmentService.SearchAppointments(model);
            if (results == null || results.Count == 0)
            {
                Console.WriteLine("No appointments found.");
            }
            else
            {
                Console.WriteLine("Appointments Found:");
                foreach (var appt in results)
                {
                    Console.WriteLine(appt);
                }
            }
        }

    }
}
