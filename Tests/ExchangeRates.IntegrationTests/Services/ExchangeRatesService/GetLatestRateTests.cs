using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ExchangeRates.Tests.IntegrationTests.Services.ExchangeRatesService
{
    [TestClass]
    public class GetLatestRateTests : ExchangeRatesServiceTestBase
    {
        [DataTestMethod]
        [DataRow("EUR", "PLN")]
        [DataRow("EUR", "GBP")]
        public async Task GetLatestRate_ShouldReturnValidRate(string srcCurrency, string dstCurrency)
        {
            // Arrange

            // Act
            var result = await ExchangeRatesService.GetLatestRateAsync(srcCurrency, dstCurrency);

            // Assert
            Assert.IsTrue(result > 0);
        }
    }
}
