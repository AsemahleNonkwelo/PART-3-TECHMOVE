using Xunit;

namespace TechMovePOE.Tests
{
    public class FileValidationTests
    {
        [Fact]
        public void PdfFile_ShouldBeValid()
        {
            // Arrange
            string fileName = "contract.pdf";

            // Act
            bool isValid = fileName.EndsWith(".pdf");

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void ExeFile_ShouldBeInvalid()
        {
            // Arrange
            string fileName = "virus.exe";

            // Act
            bool isValid = fileName.EndsWith(".pdf");

            // Assert
            Assert.False(isValid);
        }
    }
} 