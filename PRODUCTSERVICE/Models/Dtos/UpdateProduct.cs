namespace PRODUCTSERVICE.Models.Dtos
{
    public class UpdateProduct
    {
        public string Image { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
       
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
     
        public int HighestBid { get; set; }
        public DateTime EndTime { get; set; }
    }
}
