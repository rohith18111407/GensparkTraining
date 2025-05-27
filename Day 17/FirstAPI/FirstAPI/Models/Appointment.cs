﻿namespace FirstAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Notes { get; set; } = string.Empty;

        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
    }
}
