namespace SimpleTrader.WPF.Resources.Commands;

public class BuyStockCommand //: AsyncCommandBase
{
    // private readonly BuyViewModel _buyViewModel;
    // private readonly IBuyStockService _buyStockService;
    // private readonly IAccountStore _accountStore;
    //
    // public BuyStockCommand(BuyViewModel buyViewModel, IBuyStockService buyStockService, IAccountStore accountStore)
    // {
    //     _buyViewModel = buyViewModel;
    //     _buyStockService = buyStockService;
    //     _accountStore = accountStore;
    //
    //     _buyViewModel.PropertyChanged += BuyViewModel_PropertyChanged;
    // }
    //
    // public override bool CanExecute(object parameter)
    // {
    //     return _buyViewModel.CanBuyStock && base.CanExecute(parameter);
    // }
    //
    // public override async Task ExecuteAsync(object parameter)
    // {
    //     _buyViewModel.StatusMessage = string.Empty;
    //     _buyViewModel.ErrorMessage = string.Empty;
    //
    //     try
    //     {
    //         var symbol = _buyViewModel.Symbol;
    //         var shares = _buyViewModel.SharesToBuy;
    //         var account = await _buyStockService.BuyStockAsync(_accountStore.CurrentAccount, symbol, shares);
    //
    //         _accountStore.CurrentAccount = account;
    //
    //         _buyViewModel.StatusMessage = $"Successfully purchased {shares} shares of {symbol}.";
    //     }
    //     catch(InsufficientFundsException)
    //     {
    //         _buyViewModel.ErrorMessage = "Account has insufficient funds. Please transfer more money into your account.";
    //     }
    //     catch(InvalidSymbolException)
    //     {
    //         _buyViewModel.ErrorMessage = "Symbol does not exist.";
    //     }
    //     catch (Exception)
    //     {
    //         _buyViewModel.ErrorMessage = "Transaction failed.";
    //     }
    // }
    //
    // private void BuyViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    // {
    //     if (e.PropertyName == nameof(BuyViewModel.CanBuyStock))
    //     {
    //         OnCanExecuteChanged();
    //     }
    // }
}