using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PRODUCTSERVICE.Models.Dtos;
using PRODUCTSERVICE.Services.IServices;
using System.Net.Http.Headers;

namespace PRODUCTSERVICE.Services
{
    public class CartService : ICategory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CartService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;   
        }
        public async Task<CategotyDto> GetCategoryById(Guid CategoryId,String token)
        {
            var client = _httpClientFactory.CreateClient("Category");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(CategoryId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CategotyDto>(responseDto.Result.ToString());
            }
            return new CategotyDto();
        }
    }
}
