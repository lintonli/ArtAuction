namespace Auction_FrontEnd.Models
{
    public class BidResponseDto
    {
        public int BidPrice { get; set; }


        public Guid ProductId { get; set; }
    
        public Guid UserId { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public Product product { get; set; }

     
    }
}
