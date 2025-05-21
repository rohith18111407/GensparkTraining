using AppointmentApplication.Interfaces;
using AppointmentApplication.Models;
using AppointmentApplication.Repository;
using AppointmentApplication.Services;
using AppointmentApplication;

class Program
{
    static void Main(string[] args)
    {
        IRepositor<int, Appointment> repo = new AppointmentRepository();
        IAppointmentService service = new AppointmentService(repo);
        var appointment_manager = new ManageAppointment(service);
        appointment_manager.Start();
    }
}
