using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AppointmentApplication.Exceptions;
using AppointmentApplication.Interfaces;
using AppointmentApplication.Models;
using AppointmentApplication.Repository;

namespace AppointmentApplication.Services
{
    public class AppointmentService : IAppointmentService
    {
        public readonly IRepositor<int, Appointment> _repo;

        public AppointmentService(IRepositor<int, Appointment> repo)
        {
            _repo = repo;
        }

        public int AddAppointment(Appointment appointment)
        {
            //try
            //{
            //    var result = _repo.Add(appointment);
            //    return result?.Id ?? -1;
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}");
            //    return -1;
            //}

            try
            {
                if (appointment.AppointmentDate <= DateTime.Now)
                {
                    throw new AppointmentException("Appointment must be scheduled for a future date and time.");
                }

                var result = _repo.Add(appointment);
                if (result != null)
                    return result.Id;

                throw new AppointmentException("Failed to add appointment.");
            }
            catch (AppointmentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new AppointmentException("Error occurred while adding appointment.", ex);
            }
        }

        public List<Appointment>? SearchAppointments(AppointmentSearchModel searchModel)
        {
            try
            {
                var data = _repo.GetAll();

                data = SearchByName(data, searchModel.PatientName);
                data = SearchByDate(data, searchModel.AppointmentDate);
                data = SearchByAge(data, searchModel.AgeRange);

                return data.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        private ICollection<Appointment> SearchByAge(ICollection<Appointment> list, Range<int>? ageRange)
        {
            if(ageRange == null)
            {
                return list;
            }
            else
            {
                return list.Where(a => a.PatientAge >= ageRange.MinValue &&  a.PatientAge <= ageRange.MaxValue).ToList();
            }
        }

        private ICollection<Appointment> SearchByDate(ICollection<Appointment> list, DateTime? appointmentDate)
        {
            if(appointmentDate == null)
            {
                return list;
            }
            else
            {
                return list.Where(a => a.AppointmentDate.Date == appointmentDate.Value.Date).ToList();
            }
        }

        private ICollection<Appointment> SearchByName(ICollection<Appointment> list, string? patientName)
        {
            if (string.IsNullOrWhiteSpace(patientName))
            {
                return list;
            }
            else
            {
                return list.Where(a => a.PatientName.Contains(patientName, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }
    }
}
