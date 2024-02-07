namespace Auction_FrontEnd.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Guid SellerId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public string CategoryId { get; set; } = string.Empty;
        public int HighestBid { get; set; }
        public string BiddingState { get; set; } = "Active";
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; }
        public int BidCount { get; set; }
        public List<BidResponseDto> Bids { get; set; }
    }
    /* public class BiddingInfo
     {
         public Product product { get; set; }=new Product();
         public decimal YourBidPrice { get; set; }
         public bool IsWinner { get; set; }
     }*/

   
}
