namespace WareHouseFileArchiver.Models.DTOs
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}