﻿namespace BidService.Models.Dtos
{
    public class BidResponseDto
    {
        public int BidPrice { get; set; }


        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public ProductDto Product { get; set; }
    }
}
