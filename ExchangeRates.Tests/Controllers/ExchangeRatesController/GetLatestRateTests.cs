using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;


namespace ExchangeRates.Tests.Controllers.ExchangeRatesController
{
    [TestClass]
    public class GetLatestRateTests : ExchangeRatesControllerTestBase
    {
        [DataTestMethod]
        [DataRow("EUR", "PLN")]
        [DataRow("EUR", "GBP")]
        public async Task GetLastRate_ShouldReturnExpectedRate(string srcCurrency, string dstCurrency)
        {
            // Arrange  
            const float expected = 5;

            MockExchangeRatesService.Setup(service => service.GetLatestRateAsync(srcCurrency, dstCurrency)).Returns(Task.FromResult(expected));

            // Act
            var result = await ExchangeRatesController.GetLatestRate(srcCurrency, dstCurrency);

            // Assert           
            MockExchangeRatesService.Verify(service => service.GetLatestRateAsync(srcCurrency, dstCurrency), Times.Once);

            Assert.AreEqual(result, expected);
        }
    }
}
