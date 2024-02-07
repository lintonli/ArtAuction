namespace Auction_FrontEnd.Models
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public LogInUser user { get; set; }
    }
}
