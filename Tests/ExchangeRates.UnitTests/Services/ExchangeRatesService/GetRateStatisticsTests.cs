using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace ExchangeRates.Tests.UnitTests.Services.ExchangeRatesController
{
    [TestClass]
    public class GetRateStatisticsTests : ExchangeRatesServiceTestBase
    {
        [TestMethod]
        public async Task GetRateStatistics_ShouldReturnCorrectStats()
        {
            // Arrange
            var expected = new List<float> { 1, 2, 3, 4, 5 }; 

            MockExchangeRateRepository
                .Setup(repository => repository.GetRatesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(expected);
           
            // Act
            var result = await ExchangeRatesService.GetRateStatisticsAsync(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.AreEqual(result.Min, 1);
            Assert.AreEqual(result.Max, 5);
            Assert.AreEqual(result.Avg, 3);
        }
    }
}
