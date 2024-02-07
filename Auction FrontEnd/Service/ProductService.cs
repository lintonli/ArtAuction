using Auction_FrontEnd.Models;
using Auction_FrontEnd.Utility;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Auction_FrontEnd.Service
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
    
        private readonly string BASEURL = "https://localhost:7017";
        private readonly TokenProvider _tokenProvider;
        public ProductService(HttpClient httpClient, TokenProvider tokenProvider)
        {

            _httpClient = httpClient;
            _tokenProvider = tokenProvider;

        }

        public async Task<ResponseDto> AddProduct(ProductDto product)
        {
            await _tokenProvider.AttachTokenToHttpClientAsync();
            var request = JsonConvert.SerializeObject(product);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BASEURL}/api/Product", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                //change this to a list of products
                return results;

            }

            return new ResponseDto();
        }

        public async Task<ResponseDto> deleteProduct(Guid Id)
        {
            var response = await _httpClient.DeleteAsync($"{BASEURL}/api/Product/{Id}");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                //change this to a list of products
                return results;

            }

            return new ResponseDto();
        }

        public async Task<Product> GetProductByIdAsync(Guid Id)
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Product/{Id}");
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<Product>(results.Result.ToString());

            }
            return new Product();
        }

        public async Task<List<Product>> GetProductsAsync()
        {

            var response = await _httpClient.GetAsync($"{BASEURL}/api/Product");
            var content = await response.Content.ReadAsStringAsync();


            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<List<Product>>(results.Result.ToString());

            }
            return new List<Product>();
        }
        public async Task<List<Product>> GetProductbyUserId()
        {
            await _tokenProvider.AttachTokenToHttpClientAsync();
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Product/User");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<List<Product>>(content);


            /*if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<List<Product>>(results.Result.ToString());

            }*/
            return results;
        }

        public async Task<ResponseDto> updateProduct(Guid Id, ProductDto productRequestDto)
        {
            var request = JsonConvert.SerializeObject(productRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BASEURL}/api/Product/{Id}", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                //change this to a list of products
                return results;

            }

            return new ResponseDto();
        }
    }
}
