using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentApplication.Interfaces;
using AppointmentApplication.Models;

namespace AppointmentApplication.Repository
{
    public class AppointmentRepository : IRepositor<int, Appointment>
    {
        private readonly List<Appointment> _appointments = new List<Appointment>();
        private int _counter = 0;

        public Appointment? Add(Appointment item)
        {
            item.Id = ++_counter;
            _appointments.Add(item);
            return item;
        }

        public ICollection<Appointment> GetAll() => _appointments;
    }
}
