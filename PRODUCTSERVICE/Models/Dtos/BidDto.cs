namespace PRODUCTSERVICE.Models.Dtos
{
    public class BidDto
    {
        public Guid Id { get; set; }
        public int BidPrice { get; set; }


        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
