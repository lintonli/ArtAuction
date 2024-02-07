using Newtonsoft.Json;
using Auction_FrontEnd.Models;

namespace Auction_FrontEnd.Service
{
    public class CategoryService
    {
        private readonly HttpClient httpClient;
        private readonly string BASEURL = "https://localhost:7205";
        public CategoryService(HttpClient http)
        {
            httpClient = http;
        }
        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var response = await httpClient.GetAsync($"{BASEURL}/api/Category");
            var content = await response.Content.ReadAsStringAsync();

            // Assuming the response DTO has a similar structure to what you've shown
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                // Directly return the deserialized list of categories
                return JsonConvert.DeserializeObject<List<CategoryDto>>(results.Result.ToString());
            }
            return new List<CategoryDto>();
        }

    }
}
