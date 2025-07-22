
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class SaleValidatorTests
{
    private readonly SaleValidator _validator;

    public SaleValidatorTests()
    {
        _validator = new SaleValidator();
    }

    [Fact]
    public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale().Generate();
        sale.AddItem("Test Product", "TP01", 1, 10);

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Given_InvalidSale_When_Validated_Then_ShouldHaveErrors()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale().Generate();
        sale.AddItem("Test Product", "TP01", 1, 10);
        var saleType = typeof(Sale);
        saleType.GetProperty("SaleNumber")?.SetValue(sale, "");
        saleType.GetProperty("CustomerEmail")?.SetValue(sale, "invalid-email");

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SaleNumber);
        result.ShouldHaveValidationErrorFor(x => x.CustomerEmail);
    }
}
