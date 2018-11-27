using ExchangeRates.Repositories.Implementations;
using ExchangeRates.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;


namespace ExchangeRates.Tests.IntegrationTests.Controllers.ExchangeRatesController
{
    public class ExchangeRatesControllerTestBase
    {
        protected Mock<IConfiguration> MockConfiguration = new Mock<IConfiguration>();

        protected API.Controllers.ExchangeRatesController ExchangeRatesController;


        [TestInitialize]
        public void Setup()
        {
            MockConfiguration.SetupGet(configuration => configuration["AccessKey"]).Returns("6ab732f2756a83b0b9c60d6b3e552ce9");
            MockConfiguration.SetupGet(configuration => configuration["AvailableSourceCurrencies"]).Returns("EUR");
            MockConfiguration.SetupGet(configuration => configuration["AvailableDestinationCurrencies"]).Returns("PLN,GBP");

            var repository = new FixerExchangeRateRepository(MockConfiguration.Object);
            var service = new ExchangeRatesService(repository);

            ExchangeRatesController = new API.Controllers.ExchangeRatesController(service, MockConfiguration.Object);
        }
    }
}
