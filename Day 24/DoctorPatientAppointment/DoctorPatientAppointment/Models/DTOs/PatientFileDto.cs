using System.ComponentModel.DataAnnotations;

namespace DoctorPatientAppointment.Models.DTOs
{
    public class PatientFileDto
    {
        public string HospitalName { get; set; } = "Doctor Patient Appointment Hospitals";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public string PatientName { get; set; }

        [Required]
        public string PatientMobile { get; set; }

        [Required]
        public string PatientAddress { get; set; }

        [Required]
        public int PatientAge { get; set; }   
    }
}