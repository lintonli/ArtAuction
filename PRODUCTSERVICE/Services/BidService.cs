using Newtonsoft.Json;
using PRODUCTSERVICE.Models.Dtos;
using PRODUCTSERVICE.Services.IServices;

namespace PRODUCTSERVICE.Services
{
    public class BidService : IBid

    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BidService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<BidDto>> GetAllBids()
        {
            var client = _httpClientFactory.CreateClient("Bid");
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
