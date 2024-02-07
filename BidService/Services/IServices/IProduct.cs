using BidService.Models.Dtos;

namespace BidService.Services.IServices
{
    public interface IProduct
    {
        Task<ProductDto> GetProductById(Guid Id,string Token);
        Task<List<ProductDto>> GetAllProducts(string Token);
        Task<ResponseDto> updateHighest(Guid Id,UpdateHighestBidDto updateHighestBid,string Token);
      
    }
}
