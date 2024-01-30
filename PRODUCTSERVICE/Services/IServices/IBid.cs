using PRODUCTSERVICE.Models.Dtos;

namespace PRODUCTSERVICE.Services.IServices
{
    public interface IBid
    {
        Task<List<BidDto>> GetAllBids();

    }
}
