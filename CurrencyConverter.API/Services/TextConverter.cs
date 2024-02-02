using System.Globalization;
using System.Text;

namespace CurrencyConverter.API.Services
{
    public class TextConverter : ITextConverter
    {
        private static readonly string[] UnitsMap = ["zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"];
        private static readonly string[] TensMap = ["zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"];
        private static readonly string[] BigMap = ["billion", "million", "thousand", ""];
        private static readonly int[] BigMapThresholds = [1000000000, 1000000, 1000, 1];

        private static string ConvertNumberToWords(int number)
        {
            if (number == 0) return UnitsMap[0];

            var words = new StringBuilder();

            for (int i = 0; i < BigMap.Length; i++)
            {
                if ((number / BigMapThresholds[i]) > 0)
                {
                    words.Append(ConvertHundredsToWords(number / BigMapThresholds[i]) + " " + BigMap[i] + " ");
                    number %= BigMapThresholds[i];
                }
            }

            return words.ToString().Trim();
        }

        private static string ConvertHundredsToWords(int number)
        {
            if (number < 20) return UnitsMap[number];

            var words = new StringBuilder();

            if ((number / 100) > 0)
            {
                words.Append(UnitsMap[number / 100] + " hundred ");
                number %= 100;
            }

            if (number > 0)
            {
                words.Append(number < 20 ? UnitsMap[number] : TensMap[number / 10] + ((number % 10) > 0 ? "-" + UnitsMap[number % 10] : ""));
            }

            return words.ToString().Trim();
        }

        public string ConvertCurrencyToWords(decimal amount)
        {
            var amountString = amount.ToString("0.00", CultureInfo.InvariantCulture);
            var parts = amountString.Split('.');
            var dollarValue = int.Parse(parts[0]);

            if(dollarValue < 0 || dollarValue >= 1000000000)
            {
                throw new ApplicationException();
            }

            var dollars = ConvertNumberToWords(dollarValue) + (dollarValue == 1 ? " dollar" : " dollars");
            var cents = "";
            if (parts.Length > 1 && int.Parse(parts[1]) > 0)
            {
                var centValue = int.Parse(parts[1]);
                cents = " and " + ConvertNumberToWords(centValue) + (centValue == 1 ? " cent" : " cents");
            }
            return dollars + cents;
        }
    }
}
