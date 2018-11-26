using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

using ExchangeRates.Services.DTO;


namespace ExchangeRates.Tests.UnitTests.Controllers.ExchangeRatesController
{
    [TestClass]
    public class GetRateStatisticsTests : ExchangeRatesControllerTestBase
    {
        [DataTestMethod]
        [DataRow("EUR", "PLN")]
        [DataRow("EUR", "GBP")]        
        public async Task GetRateStatistics_ShouldReturnCorrectStats(string srcCurrency, string dstCurrency)
        {
            // Arrange  
            var expected = new ExchangeRateStatisticsDto
            {
                Avg = 3,
                Min = 1,
                Max = 5
            };

            MockExchangeRatesService.Setup(service => service.GetRateStatisticsAsync(srcCurrency, dstCurrency)).ReturnsAsync(expected);
            
            // Act
            var result = await ExchangeRatesController.GetRateStatistics(srcCurrency, dstCurrency);

            // Assert           
            MockExchangeRatesService.Verify(service => service.GetRateStatisticsAsync(srcCurrency, dstCurrency), Times.Once);

            Assert.AreEqual(result, expected);
        }
    }
}
