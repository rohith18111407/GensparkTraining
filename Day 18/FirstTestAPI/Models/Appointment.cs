using System.ComponentModel.DataAnnotations.Schema;

namespace FirstTestAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty ;

        //[ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }
        //[ForeignKey("PatientId")]
        public Patient? Patient { get; set; }
    }
}
