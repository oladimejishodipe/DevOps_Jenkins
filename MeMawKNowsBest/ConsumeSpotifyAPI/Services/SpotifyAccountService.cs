using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ConsumeSpotifyAPI.Services
{
    public class SpotifyAccountService:ISpotifyAccountServices
    {
        private readonly HttpClient _httpClient;


        public SpotifyAccountService(HttpClient httpClient)
        {
           _httpClient = httpClient;
        }
        public async Task<string> GetToken(string clientId, string clientSecret)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "token");
            request.Headers.Authorization= new AuthenticationHeaderValue (
                "Baseic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>

            {
                {"grant_type", "client_credentials" }
            }

              );
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
           using  var responseStream = await response.Content.ReadAsStreamAsync();
            var authResult = await JsonSerializer.DeserializeAsync<AuthResult>(responseStream);
            return authResult.access_token;
        }
    }
}
