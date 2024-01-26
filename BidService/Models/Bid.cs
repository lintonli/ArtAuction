namespace BidService.Models
{
    public class Bid
    {
        public Guid Id { get; set; }
        public int BidPrice { get; set; }
       

        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
