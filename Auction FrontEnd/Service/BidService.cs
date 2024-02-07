using Newtonsoft.Json;
using Auction_FrontEnd.Models;
using System.Text;
using Blazored.LocalStorage;
using Auction_FrontEnd.Utility;
namespace Auction_FrontEnd.Service
{
    public class BidService
    {
        private readonly HttpClient httpclient;
        private readonly string BASEURL = "https://localhost:7274";
        private readonly TokenProvider _tokenProvider;

        public BidService(HttpClient http,TokenProvider tokenProvider)
        {
            httpclient= http;
            _tokenProvider=tokenProvider;
        }
        public async Task<ResponseDto> AddBid(BidDto newBid)
        {
            

            var request = JsonConvert.SerializeObject(newBid);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //await _tokenProvider.AddTokenHeader(httpclient);
            await _tokenProvider.AttachTokenToHttpClientAsync();
            var response = await httpclient.PostAsync($"{BASEURL}/api/Bid", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                return results;
            }

            return new ResponseDto();
        }

        public async Task<List<Bid>> GetAll()
        {
            await _tokenProvider.AttachTokenToHttpClientAsync();
            var response = await httpclient.GetAsync($"{BASEURL}/api/Bid");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            return JsonConvert.DeserializeObject<List<Bid>>(results.Result.ToString());
        }

        public async Task<BidResponseDto> GetBid(Guid Id)
        {
            await _tokenProvider.AttachTokenToHttpClientAsync();
            var response = await httpclient.GetAsync($"{BASEURL}/api/Bid/{Id}");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<BidResponseDto>(content);


            return results;

        }
        public async Task<List<Bid>> GetBidbUserId()
        {
            await _tokenProvider.AttachTokenToHttpClientAsync();
            var response = await httpclient.GetAsync($"{BASEURL}/api/Bid/User");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<List<Bid>>(content);


            return results;
        }
    }
}
