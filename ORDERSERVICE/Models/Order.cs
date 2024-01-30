namespace ORDERSERVICE.Models
{
    public class Order
    {
        public Guid Id { get; set; }    
        public Guid UserId { get; set; }
        public Guid BidId {  get; set; }
        public string Status { get; set; } = "Pending";
        public string? StripeSessionId { get; set; }
        public string PaymentIntent { get; set; } = string.Empty;
    }
}
