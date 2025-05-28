namespace FirstTestAPI.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Description { get; set; } = string.Empty;

        public int DoctorId { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
