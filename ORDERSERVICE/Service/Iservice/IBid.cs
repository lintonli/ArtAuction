using ORDERSERVICE.Models.Dtos;

namespace ORDERSERVICE.Service.Iservice
{
    public interface IBid
    {
        Task<BidResponseDto> GetBidById(Guid Id,string token);
    }
}
