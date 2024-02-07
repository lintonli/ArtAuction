using System.ComponentModel.DataAnnotations;

namespace Auction_FrontEnd.Models
{
    public class ProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Image { get; set; } = string.Empty;

        [Required]
        public int Price { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime EndTime { get; set; }
    }
}
