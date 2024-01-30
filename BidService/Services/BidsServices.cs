using AutoMapper;
using BidService.Data;
using BidService.Models;
using BidService.Models.Dtos;
using BidService.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace BidService.Services
{
    public class BidsServices:IBid
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;
        public BidsServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _responseDto= new ResponseDto();    
        }

        public async Task<string> AddBid(Bid bid)
        {
            
            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();
            return "Bid Added successfully";
        }

        public async Task<string> DeleteBid(Bid bid)
        {
            _context.Bids.Remove(bid);
            await _context.SaveChangesAsync();
            return "Bid Deleted Successfully";

        }

        public async Task<List<Bid>> GetAllBids()
        {
           var bid = await _context.Bids.ToListAsync();
            return bid;
        }

        public async Task<Bid> GetBidById(Guid Id)
        {
            return await _context.Bids.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

      /*  public async Task<List<Bid>> GetBidByUserId(Guid userId)
        {
            return await _context.Bids.Where(x =>x.UserId == userId).ToListAsync();
        }*/
    }
}
