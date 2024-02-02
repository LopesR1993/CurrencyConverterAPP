using CurrencyConverter.API.Services;
using System.Globalization;

namespace CurrencyConverter.Tests
{
    public class TextConverterTests
    {
        private readonly TextConverter _textConverter = new();

        [Theory]
        [InlineData(0,"zero dollars")]
        [InlineData(1,"one dollar")]
        [InlineData(25.1, "twenty-five dollars and ten cents")]
        [InlineData(0.01, "zero dollars and one cent")]
        [InlineData(45100, "forty-five thousand one hundred dollars")]
        [InlineData(999999999.99, "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        public void ConvertCurrencyToWordsTest(decimal input, string expectedOutput)
        {

            var result = _textConverter.ConvertCurrencyToWords(input);
            
            Assert.Equal(expectedOutput, result);
        }
    }
}