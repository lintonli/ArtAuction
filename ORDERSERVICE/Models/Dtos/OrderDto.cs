namespace ORDERSERVICE.Models.Dtos
{
    public class OrderDto
    {
    
        public string Status { get; set; } = "Pending";
        public Guid Id { get; set; }
        public BidResponseDto BidResponse { get; set; }
    }
}
