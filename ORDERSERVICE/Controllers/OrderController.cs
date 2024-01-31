using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORDERSERVICE.Models;
using ORDERSERVICE.Models.Dtos;
using ORDERSERVICE.Service.Iservice;
using System.Security.Claims;

namespace ORDERSERVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrder _orderService;
        private readonly IBid _bidService;
        private readonly ResponseDto _responseDto;
        public OrderController(IMapper mapper, IOrder order, IBid bidservice)
        {
            _bidService = bidservice;
            _mapper = mapper;
            _orderService = order;
            _responseDto= new ResponseDto();
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> AddOrder(AddOrderDto dto)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _responseDto.Errormessage = "Please login to Bid";
                return Unauthorized(_responseDto);
            }
            var bid = await _bidService.GetBidById(dto.BidId);
            if (!string.IsNullOrEmpty(bid.ErrorMessage))
            {
                _responseDto.Errormessage = bid.ErrorMessage;
                return BadRequest(_responseDto);
            }
            var order = _mapper.Map<Order>(dto);
            order.UserId = Guid.Parse(UserId);
            
            var bidOrder = await _orderService.GetOrderbyBidId(dto.BidId);
           /* if(bidOrder.BidId==bid.Id)
            {
                _responseDto.Errormessage = "BidId already Exists";
                return BadRequest(_responseDto);
            }*/



            var response = await _orderService.AddOrder(order);
            _responseDto.Result=response;
            return Ok(_responseDto);

        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetOrder()
        {
            var response=await _orderService.GetOrders();
            _responseDto.Result= response;
            return Ok(_responseDto);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseDto>>GetOrderbyId(Guid Id)
        {
            var order = await _orderService.GetOrderById(Id);
            if(order == null)
            {
                return NotFound(_responseDto);
            }
            var bid = await _bidService.GetBidById(order.BidId);
            if (bid.ProductId==Guid.Empty)
            {
                return NotFound(_responseDto);
            }
           
            _responseDto.Result= order;
            return Ok(_responseDto);
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseDto>>DeleteOrder(Guid Id)
        {
            var order = await _orderService.GetOrderById(Id);
            if( order == null)
            {
                return NotFound(_responseDto);
            }
            _responseDto.Result = order;
            return Ok(_responseDto);

        }
        [HttpPost("Pay")]
        public async Task<ActionResult<ResponseDto>> MakePayments(StripeRequestDto stripeRequest)
        {
            var striperequest = await _orderService.MakePayments(stripeRequest);
            _responseDto.Result = striperequest;
            return Ok(_responseDto);
        }
        [HttpPost("validate/{Id}")]

        public async Task<ActionResult<ResponseDto>> validatePayment(Guid Id)
        {
            /*     var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];*/
            var res = await _orderService.ValidatePayments(Id);

            if (res)
            {
                _responseDto.Result = res;
                return Ok(_responseDto);
            }

            _responseDto.Errormessage = "Payment Failed!";
            return BadRequest(_responseDto);
        }

    }
}
