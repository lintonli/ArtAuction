using Auction_FrontEnd.Models;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Auction_FrontEnd.Service
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService localStorageService;
        private readonly string BASEURL = "http://localhost:5048";
        public AuthService(HttpClient http, ILocalStorageService localStorage)
        {
            _httpClient = http;
            this.localStorageService = localStorage;
        }
        public async Task<LoginResponseDto> Login(LogInUser logIn)
        {
            var request = JsonConvert.SerializeObject(logIn);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PostAsync($"{BASEURL}/api/User/login", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                var loginResponse= JsonConvert.DeserializeObject<LoginResponseDto>(results.Result.ToString());
                if (loginResponse.Token != null)
                {
                    Console.WriteLine(loginResponse);
                    await localStorageService.SetItemAsync("authToken", loginResponse.Token);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);
                }

                return loginResponse;

            }
            return new LoginResponseDto();
        }
        public async Task<ResponseDto> Register(User registerRequestDto)
        {
            var request = JsonConvert.SerializeObject(registerRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PostAsync($"{BASEURL}/api/User", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return results;

            }
            return new ResponseDto();
        }
        public async Task LogoutAsync()
        {
            await localStorageService.RemoveItemAsync("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
        public async Task<List<User>> GetUsers()
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/User/Users");
            var content = await response.Content.ReadAsStringAsync();


            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<List<User>>(results.Result.ToString());

            }
            return new List<User>();
        }
    }
}
