using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using ExchangeRates.Services.Interfaces;


namespace ExchangeRates.Tests.Controllers.ExchangeRatesController
{
    public class ExchangeRatesControllerTestBase
    {
        protected Mock<IExchangeRatesService> MockExchangeRatesService = new Mock<IExchangeRatesService>();
        protected Mock<IConfiguration> MockConfiguration = new Mock<IConfiguration>();

        protected API.Controllers.ExchangeRatesController ExchangeRatesController;


        [TestInitialize]
        public void Setup()
        {      
            MockConfiguration.SetupGet(configuration => configuration["AvailableSourceCurrencies"]).Returns("EUR");
            MockConfiguration.SetupGet(configuration => configuration["AvailableDestinationCurrencies"]).Returns("PLN,GBP");

            ExchangeRatesController = new API.Controllers.ExchangeRatesController(MockExchangeRatesService.Object, MockConfiguration.Object);
        }
    }
}
