using System.Collections.Generic;
using System.Threading.Tasks;
using FieldOps.Kernel.Functional;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Assets.Models;

namespace SimpleTrader.WPF.Features.Assets.Services;

public interface IAssetService: IRepository<Asset>
{
    Task<IEnumerable<string>> GetAssetsSearchSuggestions();
    Task<Validation<Asset>> FilterAssets(string? searchText);
}