using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CurrencyConverter.Shared.Services;
using CommunityToolkit.Maui.Alerts;

namespace CurrencyConverter.Client.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ICurrencyConverterService _currencyConverterService;

    [ObservableProperty]
    private string? currencyToConvert;

    [ObservableProperty]
    private string currencyText = string.Empty;

    public MainViewModel(ICurrencyConverterService currencyConverterService)
    {
        _currencyConverterService = currencyConverterService;
    }

    [RelayCommand]
    private async Task Convert()
    {
        try
        {
            CurrencyToConvert = CurrencyToConvert?.Replace(',', '.');
            if(CurrencyToConvert is not null && decimal.TryParse(CurrencyToConvert, out _))
            {
                var result = await _currencyConverterService.ConvertCurrencyText((decimal.Parse(CurrencyToConvert)));

                if(result is not null && result.IsSuccessStatusCode)
                {
                    CurrencyText = $"Result: {result.Content}";
                }
                else
                {
                    CurrencyText = result?.Error?.Content ?? "Error Processing the request";
                }
            }
            else
            {
                CurrencyText = "Insert a valid value. Ex:3.14";
            }
        }
        catch (Exception e)
        {
            CurrencyText = e.Message;
        }
    }
}
