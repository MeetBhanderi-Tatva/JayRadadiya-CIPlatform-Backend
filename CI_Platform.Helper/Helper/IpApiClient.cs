using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CI_Platform.Entity.Models;

namespace CI_Platform.Helper.Helper
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
