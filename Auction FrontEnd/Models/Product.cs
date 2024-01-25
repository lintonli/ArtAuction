namespace Auction_FrontEnd.Models
{
    public class Product
    {
        public Guid Id { get; set; }    
        public string Image {  get; set; }=string.Empty;
        public string Name { get; set; }= string.Empty;
        public string SellerName {  get; set; }= string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }

        public int HighestBid {  get; set; }
        public string BiddingState { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsWinner { get; set; }
        public List<Bid> Bids { get; set; } = new List<Bid>();

    }
   /* public class BiddingInfo
    {
        public Product product { get; set; }=new Product();
        public decimal YourBidPrice { get; set; }
        public bool IsWinner { get; set; }
    }*/
}
