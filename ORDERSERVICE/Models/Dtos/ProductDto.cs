namespace ORDERSERVICE.Models.Dtos
{
    public class ProductDto
    {

        public Guid Id { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Guid SellerId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public Guid CategoryId { get; set; }
        public int HighestBid { get; set; }
        public string BiddingState { get; set; } = "Active";
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; }
        public List<BidResponseDto> Bidresponse { get; set; }
    }
}
