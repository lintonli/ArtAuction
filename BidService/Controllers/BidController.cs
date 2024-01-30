using AutoMapper;
using BidService.Models;
using BidService.Models.Dtos;
using BidService.Services.IServices;
using Microsoft.AspNetCore.Authorization;
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
        private readonly BidResponseDto _bidResponse;
       
        public BidController(IBid bidService, IMapper mapper, IProduct productservice)
        {
            _bidService = bidService;
            _responseDto = new ResponseDto();
            _mapper = mapper;
            _productservice = productservice;
            _bidResponse  = new BidResponseDto();   

        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>>AddBid(BidDto newBid)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _responseDto.Errormessage = "Please login to Bid";
                return Unauthorized(_responseDto);
            }
            var product = await _productservice.GetProductById(newBid.ProductId);
            if (string.IsNullOrEmpty(product.Name))
            {
                _responseDto.Errormessage = "Invalid Values";
                return NotFound(_responseDto);
            }
            var prod = _mapper.Map<Bid>(newBid);
            prod.UserId = Guid.Parse(UserId);
            if(newBid.BidPrice<= product.HighestBid)
            {
                _responseDto.Errormessage = "Bid Higher";
                return BadRequest(_responseDto);
            }
          

            var Highestbid = await _productservice.updateHighest(product.Id, new UpdateHighestBidDto() { HighestBid = prod.BidPrice });
         
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
        [HttpGet("{Id}")]
        public async Task<ActionResult<BidResponseDto>> GetBid(Guid Id)
        { 
            var bid = await _bidService.GetBidById(Id);
            if (bid == null)
            {
                _bidResponse.ErrorMessage = "Bid does not exist";
                return NotFound(_bidResponse);
            }
            var product= await _productservice.GetProductById(bid.ProductId);
            if (string.IsNullOrEmpty(product.Name))
            {
                _bidResponse.ErrorMessage = "Product does not exist";
                return NotFound(_bidResponse);
            }

            _bidResponse.BidPrice = bid.BidPrice;
            _bidResponse.ProductId = bid.ProductId;
            _bidResponse.UserId = bid.UserId;
            _bidResponse.Product = product;

  
          
           /* var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Image = product.Image,
                HighestBid = product.HighestBid,
                BiddingState = product.BiddingState
            };*/
        
            return Ok(_bidResponse);
        }
    }
}
