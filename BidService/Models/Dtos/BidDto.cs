namespace BidService.Models.Dtos
{
    public class BidDto
    {
        public decimal BidPrice { get; set; }


        public Guid ProductId { get; set; }
      /*  public Guid UserId { get; set; }*/
    }
}
