using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimpleTrader.WPF.Features.Financials.Models;

public class FinancialModelingPrepHttpClient
{
    private readonly HttpClient _client;
    private readonly string _apiKey;

    public FinancialModelingPrepHttpClient(HttpClient client, FinancialModelingPrepAPIKey apiKey)
    {
        _client = client;
        _apiKey = apiKey.Key;
    }

    public async Task<T> GetAsync<T>(string uri)
    {
        var response = await _client.GetAsync($"{uri}?apikey={_apiKey}");
        var jsonResponse = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(jsonResponse);
    }
}