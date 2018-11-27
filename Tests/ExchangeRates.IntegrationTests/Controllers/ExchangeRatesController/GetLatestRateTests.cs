using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ExchangeRates.Tests.IntegrationTests.Controllers.ExchangeRatesController
{
    [TestClass]
    public class GetLatestRateTests : ExchangeRatesControllerTestBase
    {
        [DataTestMethod]
        [DataRow("EUR", "PLN")]
        [DataRow("EUR", "GBP")]
        public async Task GetLatestRate_ShouldReturnValidRate(string srcCurrency, string dstCurrency)
        {
            // Arrange

            // Act
            var result = await ExchangeRatesController.GetLatestRate(srcCurrency, dstCurrency);

            // Assert
            Assert.IsTrue(result > 0);
        }
    }
}
