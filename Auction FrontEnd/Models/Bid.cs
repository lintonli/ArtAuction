namespace Auction_FrontEnd.Models
{
    public class Bid
    {
        public decimal BidPrice { get; set; }
        public bool IsWinner { get; set; }
     
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

       
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
