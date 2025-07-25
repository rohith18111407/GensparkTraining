namespace WareHouseFileArchiver.Interfaces
{
    public interface IEmailService
    {
        Task SendMessageToAllUsersAsync(string subject, string message);
        Task SendEmailAsync(string to, string subject, string body);
    }
}
