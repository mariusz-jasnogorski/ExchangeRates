using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using ExchangeRates.Services.Interfaces;
using ExchangeRates.Services.DTO;


namespace ExchangeRates.API.Controllers
{
    [Produces("application/json")]
    [Route("api/ExchangeRates")]
    public class ExchangeRatesController : Controller
    {
        private readonly IExchangeRatesService _exchangeRatesService;
        private readonly IConfiguration _configuration;


        public ExchangeRatesController(IExchangeRatesService exchangeRatesService, IConfiguration configuration)
        {
            _exchangeRatesService = exchangeRatesService;
            _configuration = configuration;
        }

        /// <summary>
        /// Retrieves exchange rate for given currencies.
        /// Example: GET /api/ExchangeRates/GetLatestRate?currencyFrom=EUR&currencyTo=USD
        /// </summary>
        /// <param name="currencyFrom">Source currency</param>
        /// <param name="currencyTo">Destination currency</param>        
        /// <returns>Exchange rate</returns>
        /// <response code="200">Exchange rate</response>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(float), 200)]
        public async Task<float> GetLatestRate(string currencyFrom, string currencyTo)
        {
            ValidateCurrencies(currencyFrom, currencyTo);

            var rate = await _exchangeRatesService.GetLatestRateAsync(currencyFrom, currencyTo);

            return rate;          
        }

        /// <summary>
        /// Retrieves 7 days average, minimum and maximum for given currencies.
        /// Example: GET /api/ExchangeRates/GetRateStatistics?currencyFrom=EUR&currencyTo=PLN
        /// </summary>
        /// <param name="currencyFrom">Source currency</param>
        /// <param name="currencyTo">Destination currency</param>        
        /// <returns>Object with exchange rate statistics</returns>
        /// <response code="200">Object with exchange rate statistics</response>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ExchangeRateStatisticsDto), 200)]                
        public async Task<ExchangeRateStatisticsDto> GetRateStatistics(string currencyFrom, string currencyTo)
        {
            ValidateCurrencies(currencyFrom, currencyTo);

            var statistics = await _exchangeRatesService.GetRateStatisticsAsync(currencyFrom, currencyTo);

            return statistics;
        }

        private void ValidateCurrencies(string currencyFrom, string currencyTo)
        {    
            var availableSourceCurrencies = _configuration["AvailableSourceCurrencies"];
            var availableDestinationCurrencies = _configuration["AvailableDestinationCurrencies"];

            if (!availableSourceCurrencies.Contains(currencyFrom.ToUpper()))
            {
                throw new Exception($"Source currency {currencyFrom} is invalid or not available.");
            }

            if (!availableDestinationCurrencies.Contains(currencyTo.ToUpper()))
            {
                throw new Exception($"Destination currency {currencyTo} is invalid or not available.");
            }
        }
    }
}
