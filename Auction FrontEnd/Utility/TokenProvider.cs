using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace Auction_FrontEnd.Utility
{
    public class TokenProvider
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;
        public TokenProvider(HttpClient client, ILocalStorageService localStorage)
        {
            this.httpClient = client;
            this.localStorageService = localStorage;
        }
        public async Task<string> GetTokenAsync(string token)
        {
            return await localStorageService.GetItemAsync<string>(token);
        }

        public async Task SaveTokenAsync(string token)
        {
            await localStorageService.SetItemAsync(token, token);
        }
        public async Task AttachTokenToHttpClientAsync()
        {
            var token = await GetTokenAsync("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
        public async Task RemoveItemAsync()
        {
            await localStorageService.RemoveItemAsync("authToken");
        }
        public  async Task AddTokenHeader(HttpClient client)
        {
             var token = await GetTokenAsync("authToken");
            client.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer"+ token);
            
        }
    }
}
