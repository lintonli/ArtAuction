﻿using BidService.Models.Dtos;

namespace BidService.Services.IServices
{
    public interface IProduct
    {
        Task<ProductDto> GetProductById(Guid Id);
        Task<ResponseDto> updateHighest(Guid Id,UpdateHighestBidDto updateHighestBid);
      
    }
}
