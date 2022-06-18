using LondonStock.API.Business.Validations;
using LondonStock.API.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LondonStock.API.Business.UnitTests.Validations
{
    [TestClass]
    public class StockRequestValidatorTests
    {
        private StockRequestValidator _sut;

        private StockRequest _stockRequest;

        [TestInitialize]
        public void TestInit()
        {
            _stockRequest = new StockRequest { BrockerId = "B1234", TotalShare = 100, Price = 200 };

            _sut = new StockRequestValidator();
        }

        [TestMethod]
        public void WhenRequestIsValid_ThenValidIsTrue()
        {
            // Arrange

            // Act
            var result = _sut.Validate(_stockRequest);

            // Assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void WhenBrockerIdIsNull_ThenValidIsFalse()
        {
            // Arrange
            _stockRequest.BrockerId = null;

            // Act
            var result = _sut.Validate(_stockRequest);

            // Assert
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void WhenPriceIsZero_ThenValidIsFalse()
        {
            // Arrange
            _stockRequest.Price = 0;

            // Act
            var result = _sut.Validate(_stockRequest);

            // Assert
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void WhenTotalShareIsZero_ThenValidIsFalse()
        {
            // Arrange
            _stockRequest.TotalShare = 0;

            // Act
            var result = _sut.Validate(_stockRequest);

            // Assert
            Assert.IsFalse(result.IsValid);
        }
    }
}
