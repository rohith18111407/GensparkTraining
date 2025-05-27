using FirstAPI.Interfaces;
using FirstAPI.Models;

namespace FirstAPI.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly List<Appointment> _appointments = new List<Appointment>();
        private int _nextId = 1;
        public void Add(Appointment appointment)
        {
            bool exists = _appointments.Any(a =>
                a.DoctorId == appointment.DoctorId &&
                a.PatientId == appointment.PatientId &&
                a.AppointmentDate == appointment.AppointmentDate
            );
            if (exists)
            {
                throw new InvalidOperationException("Duplicate Appontment Exists");
            }
            appointment.Id = _nextId++;
            _appointments.Add(appointment);
        }

        public void Delete(int id)
        {
            var existingAppointment = _appointments.FirstOrDefault(a => a.Id == id);
            if (existingAppointment != null)
            {
                _appointments.Remove(existingAppointment);
            }
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _appointments;
        }

        public Appointment? GetById(int id)
        {
            return _appointments.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Appointment appointment)
        {
            var existingAppointment = _appointments.FirstOrDefault(a => a.Id == appointment.Id);
            if (existingAppointment != null)
            {
                existingAppointment.DoctorId = appointment.DoctorId;
                existingAppointment.PatientId = appointment.PatientId;
                existingAppointment.AppointmentDate = appointment.AppointmentDate;
                existingAppointment.Notes = appointment.Notes;
            }
        }
    }
}
