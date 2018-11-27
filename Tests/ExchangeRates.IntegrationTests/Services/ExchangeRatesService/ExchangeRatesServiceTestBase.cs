using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ExchangeRates.Repositories.Interfaces;
using ExchangeRates.Repositories.Implementations;


namespace ExchangeRates.Tests.IntegrationTests.Services.ExchangeRatesService
{
    public class ExchangeRatesServiceTestBase
    {
        protected Mock<IConfiguration> MockConfiguration = new Mock<IConfiguration>();

        protected ExchangeRates.Services.Implementations.ExchangeRatesService ExchangeRatesService;


        [TestInitialize]
        public void Setup()
        {
            MockConfiguration.SetupGet(configuration => configuration["AccessKey"]).Returns("6ab732f2756a83b0b9c60d6b3e552ce9");

            var repository = new FixerExchangeRateRepository(MockConfiguration.Object);

            ExchangeRatesService = new ExchangeRates.Services.Implementations.ExchangeRatesService(repository);
        }
    }
}
