namespace MoodJournal.Interfaces
{
    public interface ILLMService
    {
        Task<string> GenerateTipAsync(string mood, string note);
    }
}