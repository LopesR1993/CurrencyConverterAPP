namespace CurrencyConverter.API.Services
{
    public interface ITextConverter
    {
        string ConvertCurrencyToWords(decimal amount);
    }
}