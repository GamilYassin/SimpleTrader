#region

using System.Windows;
using System.Windows.Controls;
using SimpleTrader.WPF.Features.Assets.Models;

#endregion

namespace SimpleTrader.WPF.Resources.Controls;

public partial class AssetInfoCard : UserControl
{
    public AssetInfoCard()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty AssetProperty =
        DependencyProperty.Register(nameof(Asset), typeof(Asset), typeof(AssetInfoCard), new PropertyMetadata(null));

    public Asset? Asset
    {
        get => (Asset?)GetValue(AssetProperty);
        set => SetValue(AssetProperty, value);
    }
}