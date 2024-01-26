using AutoMapper;
using BidService.Models;
using BidService.Models.Dtos;
using BidService.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace BidService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBid _bidService;
        private readonly IProduct _productservice;
        private readonly ResponseDto _responseDto;
        private readonly IMapper _mapper;
        public BidController(IBid bidService, IMapper mapper, IProduct productservice)
        {
            _bidService = bidService;
            _responseDto = new ResponseDto();
            _mapper = mapper;
            _productservice = productservice;

        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto>>AddBid(BidDto newBid)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _responseDto.Errormessage = "Please login to Bid";
                return Unauthorized(_responseDto);
            }
            var category = await _productservice.GetProductById(newBid.ProductId);
            if (category == null)
            {
                _responseDto.Errormessage = "Invalid Values";
                return NotFound(_responseDto);
            }
            var prod = _mapper.Map<Bid>(newBid);
            prod.UserId = Guid.Parse(UserId);


            var res = await _bidService.AddBid(prod);
            _responseDto.Result = res;
            return Created($"{prod.Id}", _responseDto);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAll()
        {
            var res =await _bidService.GetAllBids();
            _responseDto.Result= res;
            return Ok(_responseDto);
        }
    }
}
