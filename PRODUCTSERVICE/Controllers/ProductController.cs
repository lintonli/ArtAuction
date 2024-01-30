using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRODUCTSERVICE.Models;
using PRODUCTSERVICE.Models.Dtos;
using PRODUCTSERVICE.Services.IServices;
using System.Security.Claims;

namespace PRODUCTSERVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProducts _productServices;
        private readonly IBid _bidServices;
        private readonly ICategory _cartService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;
        public ProductController(IProducts productServices, IMapper mapper, ICategory cartService, IBid bidServices)
        {
            _mapper = mapper;
            _productServices = productServices;
            _responseDto = new ResponseDto();
            _cartService = cartService;
            _bidServices = bidServices;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> AddProduct(AddProductDto productDto)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _responseDto.Errormessage = "Please login to add art";
                return Unauthorized(_responseDto);
            }
           
            var category = await _cartService.GetCategoryById(productDto.CategoryId);
            if (category == null)
            {
                _responseDto.Errormessage = "Invalid Values";
                return NotFound(_responseDto);
            }
            var prod = _mapper.Map<Product>(productDto);
            prod.HighestBid = prod.Price;
            prod.SellerId = Guid.Parse(UserId);


            var res = await _productServices.AddProduct(prod);
            _responseDto.Result = res;
            return Created($"{prod.Id}", _responseDto);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllProducts()
        {
            var res = await _productServices.GetProducts();
            var products= _mapper.Map<List<ProductDto>>(res);

            var bids = await _bidServices.GetAllBids(); 
            foreach(var prod in products)
            {
                var productbids = bids.FindAll(bid => bid.ProductId == prod.Id);
                prod.BidCount = productbids.Count();

            }
            _responseDto.Result = products;
            return Ok(_responseDto);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseDto>> GetProduct(Guid Id)
        {
            var prod = await _productServices.GetProductById(Id);
            if (prod == null)
            {
                return NotFound(_responseDto);
            }
            var bidproducts= _mapper.Map<ProductDto>(prod);

            var bid = await _bidServices.GetAllBids();
            var productBids = bid.FindAll(bid => bid.ProductId == prod.Id);
            bidproducts.BidCount = productBids.Count;

            _responseDto.Result = prod;
            return Ok(_responseDto);
        }
        [HttpDelete("{Id}")]
        public async Task<string> DeleteProduct(Guid Id)
        {
            var prod = await _productServices.GetProductById(Id);
            if (prod == null)
            {
                return "Product not found";
            }
            var res = await _productServices.DeleteProduct(prod);
            return "Product Deleted Successfully";
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<ResponseDto>> UpdateProduct(Guid Id, UpdateProduct update)
        {
            var res = await _productServices.GetProductById(Id);
            if (res == null)
            {
                _responseDto.Errormessage = "Product not found";
                return NotFound(_responseDto);
            }

            var prod= _mapper.Map<Product>(update);
           
            var prods = await _productServices.UpdateProduct(prod);
            _responseDto.Errormessage = "Product Updated Successfully";
            return Ok(_responseDto) ;
        }

        [HttpPut("UpdateHighestBid/{Id}")]
        public async Task<ActionResult<bool>> UpdateHighestBid(Guid Id, HighestBidDto newBid)
        {
            var res = await _productServices.GetProductById(Id);
            if (res == null)
            {
                _responseDto.Errormessage = "Product not found";
                return NotFound(_responseDto);
            }
            res.HighestBid = newBid.HighestBid;
            var Updatebid = await _productServices.UpdateProduct(res);
            if(Updatebid != null)
            {
                _responseDto.IsSuccess= true;
                return Ok(_responseDto) ;
            }
            return BadRequest(_responseDto);

           /* var bid = await _productServices.UpdateHighestBid(Id, newBid.HighestBid);
            _responseDto.IsSuccess = true;
            return Ok(_responseDto) ;*/

          
        }
       /*[HttpGet("bidcount{Id}")]
        public async Task<ActionResult<bool>> BidCount(Guid Id, ProductDto dto)
        {
            var product = await _productServices.GetProductById(Id);
            if (product == null)
            {
                _responseDto.Errormessage = "Product Not Found";
                return NotFound(_responseDto);
            }
            product.BidCount=dto.BidCount;
            var updatecount = await _productServices.UpdateProduct(product);
            if (updatecount != null)
            {
                _responseDto.IsSuccess= true;
                return Ok(_responseDto) ;
            }
            return BadRequest(_responseDto);
        }*/
    }
}
