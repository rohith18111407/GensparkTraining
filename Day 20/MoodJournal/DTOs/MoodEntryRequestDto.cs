namespace MoodJournal.DTOs
{
    public class MoodEntryRequestDto
    {
        public string Mood { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}