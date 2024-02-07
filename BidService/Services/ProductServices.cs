using BidService.Models.Dtos;
using BidService.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BidService.Services
{
    public class ProductServices : IProduct
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductServices(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
        }

        public async Task<List<ProductDto>> GetAllProducts(string Token)
        {
            var client = _httpClientFactory.CreateClient("Products");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            var response = await client.GetAsync(ToString());
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<ProductDto>>(responseDto.Result.ToString());
            }
            return new List<ProductDto>();
        }

        public async Task<ProductDto> GetProductById(Guid Id, string Token)
        {
            var client = _httpClientFactory.CreateClient("Products");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            var response = await client.GetAsync(Id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProductDto>(responseDto.Result.ToString());
            }
            return new ProductDto();
        }


        public async Task<ResponseDto> updateHighest(Guid Id,UpdateHighestBidDto updateHighestBid, string Token)
        {
            var client = _httpClientFactory.CreateClient("Products");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            var content= JsonConvert.SerializeObject(updateHighestBid);
            var stringcontent= new StringContent(content, Encoding.UTF8, "application/json");
            var response= await client.PutAsync($"UpdateHighestBid/{Id}", stringcontent);

            var responsecontent= await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responsecontent);
            return responseDto;
            /*var response = await client.GetAsync(updateHighestBid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UpdateHighestBidDto>(responseDto.Result.ToString());
            }
            return new UpdateHighestBidDto();*/

        }
    }

}
