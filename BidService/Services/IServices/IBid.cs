using BidService.Models;

namespace BidService.Services.IServices
{
    public interface IBid
    {
        Task<string>AddBid(Bid bid);
        Task<List<Bid>>GetAllBids();
        Task<Bid> GetBidById(Guid Id);
        Task<string> DeleteBid(Bid bid);
       Task<List<Bid>>GetBidByUserId(Guid userId);
    }
}
