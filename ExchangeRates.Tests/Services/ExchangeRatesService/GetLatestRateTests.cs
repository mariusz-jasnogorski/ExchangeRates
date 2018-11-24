using Moq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ExchangeRates.Tests.Services.ExchangeRatesService
{
    [TestClass]
    public class GetLatestRateTests : ExchangeRatesServiceTestBase
    {
        [TestMethod]
        public async Task GetLatestRate_ShouldReturnExpectedRate()
        {
            // Arrange
            const float expected = 5;

            MockExchangeRateRepository.Setup(repository => repository.GetLatestRateAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(expected));           

            // Act
            var result = await ExchangeRatesService.GetLatestRateAsync(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.AreEqual(result, expected);
        }
    }
}
