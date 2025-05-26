namespace PatientAPI.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age {  get; set; }
        public string Address { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;

    }
}
