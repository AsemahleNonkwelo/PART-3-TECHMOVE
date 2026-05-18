using Xunit;

namespace TechMovePOE.Tests
{
    public class CurrencyServiceTests
    {
        [Fact]
        public void ConvertUsdToZar_ReturnsCorrectAmount()
        {
            // Arrange
            decimal usd = 100;
            decimal exchangeRate = 18.50m;

            // Act
            decimal zar = usd * exchangeRate;

            // Assert
            Assert.Equal(1850m, zar);
        }
    }
}
