namespace MoodJournal.Models
{
    public class MoodEntry
    {
        public int Id { get; set; } // Auto-increment
        public DateTime Date { get; set; } = DateTime.Now;
        public string Mood { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public string Tip { get; set; } = string.Empty;
    }
}