using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class SaleItemValidatorTests
{
    private readonly SaleItemValidator _validator;

    public SaleItemValidatorTests()
    {
        _validator = new SaleItemValidator();
    }

    [Fact]
    public void Given_ValidSaleItem_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var saleItem = new SaleItem("Test Product", "TP01", 1, 10);

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Given_InvalidSaleItem_When_Validated_Then_ShouldHaveErrors()
    {
        // Arrange
        var saleItem = new SaleItem("Valid Product", "VP01", 1, 10);
        var type = typeof(SaleItem);
        type.GetProperty("ProductName")?.SetValue(saleItem, "");
        type.GetProperty("ProductCode")?.SetValue(saleItem, "");
        type.GetProperty("Quantity")?.SetValue(saleItem, 0);
        type.GetProperty("UnitPrice")?.SetValue(saleItem, -1m);

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductName);
        result.ShouldHaveValidationErrorFor(x => x.ProductCode);
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
        result.ShouldHaveValidationErrorFor(x => x.UnitPrice);
    }
}
