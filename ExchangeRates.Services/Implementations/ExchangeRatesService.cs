using System;
using System.Threading.Tasks;
using System.Linq;

using ExchangeRates.Repositories.Interfaces;
using ExchangeRates.Services.DTO;
using ExchangeRates.Services.Interfaces;


namespace ExchangeRates.Services.Implementations
{
    public class ExchangeRatesService : IExchangeRatesService
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;


        public ExchangeRatesService(IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
        }


        public async Task<float> GetLatestRateAsync(string currencyFrom, string currencyTo)
        {
            return await _exchangeRateRepository.GetLatestRateAsync(currencyFrom, currencyTo);
        }

        public async Task<ExchangeRateStatisticsDto> GetRateStatisticsAsync(string currencyFrom, string currencyTo)
        {
            var rates = (await _exchangeRateRepository.GetRatesAsync(currencyFrom, currencyTo, DateTime.Now.AddDays(-7), DateTime.Now)).ToList();

            return new ExchangeRateStatisticsDto
            {
                Avg = rates.Average(),
                Min = rates.Min(),
                Max = rates.Max()
            };
        }
    }
}
