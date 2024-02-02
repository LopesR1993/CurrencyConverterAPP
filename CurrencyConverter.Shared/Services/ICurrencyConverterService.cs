using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Shared.Services
{
    public interface ICurrencyConverterService
    {
        [Get("/currency/{amount}")]
        Task<ApiResponse<string>> ConvertCurrencyText(decimal amount);
    }
}
