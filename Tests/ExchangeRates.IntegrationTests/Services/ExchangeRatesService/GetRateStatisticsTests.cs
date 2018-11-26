using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Tests.IntegrationTests.Services.ExchangeRatesService
{
    [TestClass]
    public class GetRateStatisticsTests : ExchangeRatesServiceTestBase
    {
        [DataTestMethod]
        [DataRow("EUR", "PLN")]
        [DataRow("EUR", "GBP")]
        public async Task GetRateStatistics_ShouldReturnValidStats(string srcCurrency, string dstCurrency)
        {
            // Arrange

            // Act
            var result = await ExchangeRatesService.GetRateStatisticsAsync(srcCurrency, dstCurrency);

            // Assert
            Assert.IsTrue(result.Min > 0);
            Assert.IsTrue(result.Max > 0);
            Assert.IsTrue(result.Avg > 0);
            Assert.IsTrue(result.Min <= result.Max);
            Assert.IsTrue((result.Avg >= result.Min));
            Assert.IsTrue((result.Avg <= result.Max));
        }
    }
}
