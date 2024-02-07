using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PRODUCTSERVICE.Models.Dtos;
using PRODUCTSERVICE.Services.IServices;
using System.Net.Http.Headers;

namespace PRODUCTSERVICE.Services
{
    public class BidService : IBid

    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BidService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<BidDto>> GetAllBids(string token)
        {
            var client = _httpClientFactory.CreateClient("Bid");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("");
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BidDto>>(responseDto.Result.ToString());
            }
            return new List<BidDto>();
        }
    }
}
