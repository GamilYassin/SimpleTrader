using System;
using System.Threading.Tasks;
using SimpleTrader.WPF.Features.Assets.Enums;
using SimpleTrader.WPF.Features.Assets.Models;
using SimpleTrader.WPF.Features.Assets.Services;
using SimpleTrader.WPF.Features.Financials.Models;

namespace SimpleTrader.WPF.Features.Financials.Services;

public class MajorIndexService : IMajorIndexService
{
    private readonly FinancialModelingPrepHttpClient _client;

    public MajorIndexService(FinancialModelingPrepHttpClient client)
    {
        _client = client;
    }

    public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
    {
        var uri = "majors-indexes/" + GetUriSuffix(indexType);

        var majorIndex = await _client.GetAsync<MajorIndex>(uri);
        majorIndex.Type = indexType;

        return majorIndex;
    }

    private string GetUriSuffix(MajorIndexType indexType)
    {
        switch(indexType)
        {
            case MajorIndexType.DowJones:
                return ".DJI";
            case MajorIndexType.Nasdaq:
                return ".IXIC";
            case MajorIndexType.SP500:
                return ".INX";
            default:
                throw new Exception("MajorIndexType does not have a suffix defined.");
        }
    }
}