using Skyline.Utilities.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace Skyline.Utilities.UnitTests.Validations
{
    [Trait("工具类", "DataAnnotations验证")]
    public class DataAnnotationsValidatorTest
    {
        [Fact(DisplayName = "验证成功-正常入参")]
        public void Validator_ReturnTrue_WithExpectedParameters()
        {
            // Arrange
            DataAnnotationsSampleModel model = new DataAnnotationsSampleModel
            {
                Int32Value = 101,
                StringValue = "测试"
            };
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = DataAnnotationsValidator.Validate(model, validationResults);

            // Assert
            Assert.True(isValid);
            Assert.True(validationResults?.Count == 0);
        }

        [Theory(DisplayName = "验证失败-正常入参")]
        [InlineData("测试测试A", "长度不能超过4")]
        [InlineData("A", "长度不能小于2")]
        public void Validator_ReturnFalse_WithExpectedParameters(string stringVal, string errorMessage)
        {
            // Arrange
            DataAnnotationsSampleModel model = new DataAnnotationsSampleModel
            {
                Int32Value = 101,
                StringValue = stringVal
            };
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = DataAnnotationsValidator.Validate(model, validationResults);

            // Assert
            Assert.False(isValid);
            Assert.True(validationResults?.Count == 1);
            Assert.True(validationResults[0].ErrorMessage.Equals(errorMessage));
        }

        [Fact(DisplayName = "抛出参数空异常-空值入参")]
        public void Validator_ThrowArgumentNullException_WithNull()
        {
            // Arrange
            DataAnnotationsSampleModel model = null;
            var validationResults = new List<ValidationResult>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                DataAnnotationsValidator.Validate(model, validationResults);
            });
        }
    }
}
