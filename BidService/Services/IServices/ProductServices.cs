using BidService.Models.Dtos;
using Newtonsoft.Json;

namespace BidService.Services.IServices
{
    public class ProductServices : IProduct
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductServices(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
        }
        public async Task<ProductDto> GetProductById(Guid Id)
        {
            var client = _httpClientFactory.CreateClient("Products");
            var response = await client.GetAsync(Id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProductDto>(responseDto.Result.ToString());
            }
            return new ProductDto();
        }
    }
    
}
