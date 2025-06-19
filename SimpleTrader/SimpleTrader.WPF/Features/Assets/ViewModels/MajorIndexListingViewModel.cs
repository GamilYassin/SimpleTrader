using System.Windows.Input;
using SimpleTrader.WPF.Features.Assets.Models;
using SimpleTrader.WPF.Features.Assets.Services;
using SimpleTrader.WPF.Features.Financials.Services;
using SimpleTrader.WPF.Features.Home.ViewModels;
using SimpleTrader.WPF.Resources.Commands;

namespace SimpleTrader.WPF.Features.Assets.ViewModels;

public class MajorIndexListingViewModel : ViewModelBase
{
    private MajorIndex? _dowJones;
    public MajorIndex? DowJones
    {
        get
        {
            return _dowJones;
        }
        set
        {
            _dowJones = value;
            OnPropertyChanged(nameof(DowJones));
        }
    }

    private MajorIndex? _nasdaq;
    public MajorIndex? Nasdaq
    {
        get
        {
            return _nasdaq;
        }
        set
        {
            _nasdaq = value;
            OnPropertyChanged(nameof(Nasdaq));
        }
    }

    private MajorIndex? _sp500;
    public MajorIndex? SP500
    {
        get
        {
            return _sp500;
        }
        set
        {
            _sp500 = value;
            OnPropertyChanged(nameof(SP500));
        }
    }

    private bool _isLoading;
    public bool IsLoading
    {
        get
        {
            return _isLoading;
        }
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    public ICommand LoadMajorIndexesCommand { get; }

    public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
    {
        LoadMajorIndexesCommand = new LoadMajorIndexesCommand(this, majorIndexService);
    }

    public static MajorIndexListingViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
    {
        var majorIndexViewModel = new MajorIndexListingViewModel(majorIndexService);

        majorIndexViewModel.LoadMajorIndexesCommand.Execute(null);

        return majorIndexViewModel;
    }
}