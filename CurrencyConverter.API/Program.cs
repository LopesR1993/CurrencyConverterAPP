using CurrencyConverter.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITextConverter, TextConverter>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/currency/{amount:decimal}", (decimal amount, ITextConverter textConverter) =>
{
    try
    {
        var result = textConverter.ConvertCurrencyToWords(amount);
        return Results.Ok(result);
    }
    catch (Exception)
    {
        return Results.BadRequest("Invalid input. Numbers should be between 0 and 999999999,99");
    }
});


app.Run();

