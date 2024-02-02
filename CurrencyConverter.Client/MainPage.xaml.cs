using CurrencyConverter.Client.ViewModels;

namespace CurrencyConverter.Client;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _mainViewModel;

    public MainPage(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        BindingContext = _mainViewModel;
    }
}
