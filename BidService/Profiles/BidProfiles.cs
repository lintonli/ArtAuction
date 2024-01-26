using AutoMapper;
using BidService.Models;
using BidService.Models.Dtos;

namespace BidService.Profiles
{
    public class BidProfiles:Profile
    {
        public BidProfiles()
        {
            CreateMap<BidDto, Bid>().ReverseMap();
        }
    }
}
