using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

using ExchangeRates.Repositories.Interfaces;


namespace ExchangeRates.Repositories.Implementations
{
    public class FixerExchangeRateRepository : IExchangeRateRepository
    {
        private readonly IConfiguration _configuration;


        public FixerExchangeRateRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<float> GetLatestRateAsync(string currencyFrom, string currencyTo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://data.fixer.io/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/latest?access_key={_configuration["AccessKey"]}&base={currencyFrom}&symbols={currencyTo}");

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var jsonResult = await response.Content.ReadAsStringAsync();

                        var deserializedJsonResult = JObject.Parse(jsonResult);

                        var success = deserializedJsonResult["success"].Value<bool>();

                        if (success)
                        {
                            var rate = deserializedJsonResult["rates"][currencyTo];

                            return rate.Value<float>();
                        }
                        else
                        {
                            var errorCode = deserializedJsonResult["error"]["code"].Value<string>();
                            var errorType = deserializedJsonResult["error"]["type"].Value<string>();

                            throw new Exception($"Error code: {errorCode}. Error type: {errorType}.");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to retrieve latest exchange rate.", ex);
                    }
                }
                else
                {
                    throw new Exception($"Failed to get latest exchange rate. Reason: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<IEnumerable<float>> GetRatesAsync(string currencyFrom, string currencyTo, DateTime dateFrom, DateTime dateTo)
        {           
            if (dateFrom.Date > dateTo.Date)
            {
                throw new Exception("Incorrect date range.");
            }

            var result = new List<float>();

            var requestTasks = new List<Task<float>>();

            var requestDate = dateFrom.Date;

            while (requestDate.Date < dateTo.Date)
            {
                requestTasks.Add(GetRateForDate(requestDate, currencyFrom, currencyTo));

                requestDate = requestDate.AddDays(1);
            }

            while (requestTasks.Count > 0)
            {
                Task<float> firstFinishedTask = await Task.WhenAny(requestTasks);
           
                requestTasks.Remove(firstFinishedTask);

                result.Add(await firstFinishedTask);
            }

            return result;
        }

        protected async Task<float> GetRateForDate(DateTime date, string currencyFrom, string currencyTo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://data.fixer.io/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/{date.ToString("yyyy-MM-dd")}?access_key={_configuration["AccessKey"]}&&base={currencyFrom}&symbols={currencyTo}");

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var jsonResult = await response.Content.ReadAsStringAsync();

                        var deserializedJsonResult = JObject.Parse(jsonResult);

                        var success = deserializedJsonResult["success"].Value<bool>();

                        if (success)
                        {
                            var rate = deserializedJsonResult["rates"][currencyTo];

                            return rate.Value<float>();
                        }
                        else
                        {
                            var errorCode = deserializedJsonResult["error"]["code"].Value<string>();
                            var errorType = deserializedJsonResult["error"]["type"].Value<string>();

                            throw new Exception($"Error code: {errorCode}. Error type: {errorType}.");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to retrieve historical exchange rate.", ex);
                    }
                }                
                
                throw new Exception($"Failed to get historical exchange rate. Reason: {response.ReasonPhrase}");                
            }
        }        
    }   
}
