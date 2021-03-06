﻿using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;

using ExchangeRates.Repositories.Interfaces;


namespace ExchangeRates.Tests.UnitTests.Services.ExchangeRatesController
{

    public class ExchangeRatesServiceTestBase
    {
        protected Mock<IExchangeRateRepository> MockExchangeRateRepository = new Mock<IExchangeRateRepository>();

        protected ExchangeRates.Services.Implementations.ExchangeRatesService ExchangeRatesService;

        [TestInitialize]
        public void Setup()
        {
            ExchangeRatesService = new ExchangeRates.Services.Implementations.ExchangeRatesService(MockExchangeRateRepository.Object);
        }
    }
}