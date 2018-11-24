using System;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace ExchangeRates.Repositories.Interfaces
{
    public interface IExchangeRateRepository
    {
        Task<float> GetLatestRateAsync(string currencyFrom, string currencyTo);

        Task<IEnumerable<float>> GetRatesAsync(string currencyFrom, string currencyTo, DateTime dateFrom, DateTime dateTo);
    }
}
