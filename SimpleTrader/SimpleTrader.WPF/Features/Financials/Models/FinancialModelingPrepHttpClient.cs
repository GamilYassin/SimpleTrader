using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimpleTrader.WPF.Features.Financials.Models;

public class FinancialModelingPrepHttpClient(HttpClient client, FinancialModelingPrepApiKey apiKey)
{
    private readonly string _apiKey = apiKey.Key;

    public async Task<T?> GetAsync<T>(string uri)
    {
        var response = await client.GetAsync($"{uri}?apikey={_apiKey}");
        var jsonResponse = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(jsonResponse);
    }
}