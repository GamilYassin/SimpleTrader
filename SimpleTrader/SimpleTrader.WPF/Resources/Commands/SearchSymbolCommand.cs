﻿namespace SimpleTrader.WPF.Resources.Commands;

public class SearchSymbolCommand // : AsyncCommandBase
{
    // private readonly ISearchSymbolViewModel _viewModel;
    // private readonly IStockPriceService _stockPriceService;
    //
    // public SearchSymbolCommand(ISearchSymbolViewModel viewModel, IStockPriceService stockPriceService)
    // {
    //     _viewModel = viewModel;
    //     _stockPriceService = stockPriceService;
    //
    //     _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    // }
    //
    // public override bool CanExecute(object parameter)
    // {
    //     return _viewModel.CanSearchSymbol && base.CanExecute(parameter);
    // }
    //
    // public override async Task ExecuteAsync(object parameter)
    // {
    //     try
    //     {
    //         var stockPrice = await _stockPriceService.UpdateAssetPriceAsync(_viewModel.Symbol);
    //
    //         _viewModel.SearchResultSymbol = _viewModel.Symbol.ToUpper();
    //         _viewModel.StockPrice = stockPrice;
    //     }
    //     catch (InvalidSymbolException)
    //     {
    //         _viewModel.ErrorMessage = "Symbol does not exist.";
    //     }
    //     catch (Exception)
    //     {
    //         _viewModel.ErrorMessage = "Failed to get symbol information.";
    //     }
    // }
    //
    // private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    // {
    //     if (e.PropertyName == nameof(ISearchSymbolViewModel.CanSearchSymbol))
    //     {
    //         OnCanExecuteChanged();
    //     }
    // }
}