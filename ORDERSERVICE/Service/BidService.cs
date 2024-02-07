using Newtonsoft.Json;
using ORDERSERVICE.Models.Dtos;
using ORDERSERVICE.Service.Iservice;
using System.Net.Http.Headers;

namespace ORDERSERVICE.Service
{
    public class BidService : IBid
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BidService(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
        }
        public async Task<BidResponseDto> GetBidById(Guid Id,string token)
        {
            var client = _httpClientFactory.CreateClient("Bid");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"{Id}");
            var context = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<BidResponseDto>(context);            
            if (response.IsSuccessStatusCode)
            {
                return responseDto;
            }
            return new BidResponseDto();
        }
    } 
}
