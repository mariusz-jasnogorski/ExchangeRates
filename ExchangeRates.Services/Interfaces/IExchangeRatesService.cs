using System.Threading.Tasks;

using ExchangeRates.Services.DTO;


namespace ExchangeRates.Services.Interfaces
{
    public interface IExchangeRatesService
    {
        Task<float> GetLatestRateAsync(string currencyFrom, string currencyTo);

        Task<ExchangeRateStatisticsDto> GetRateStatisticsAsync(string currencyFrom, string currencyTo);
    }
}
