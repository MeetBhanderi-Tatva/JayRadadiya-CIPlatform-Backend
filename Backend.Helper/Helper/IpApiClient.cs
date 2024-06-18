using CI_Platform.Entity.Models;
using CI_Platform.Entity.ResponseModel;
using System.Net.Http.Json;

namespace CI_Platform.Backend.Helper
{
    public class IpApiClient(HttpClient httpClient)
    {
        private const string BASE_URL = "http://ip-api.com";
        private readonly HttpClient _httpClient = httpClient;

        public async Task<IpApiResponse?> Get(string? ipAddress, CancellationToken ct)
        {
            var route = $"{BASE_URL}/json/{ipAddress}";
            var res = await _httpClient.GetFromJsonAsync<IpApiResponse>(route, ct);
            return res;
        }
    }
}
