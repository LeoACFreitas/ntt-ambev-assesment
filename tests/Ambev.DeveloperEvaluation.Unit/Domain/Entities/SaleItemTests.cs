
using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleItemTests
{
    [Fact]
    public void Given_QuantityLessThan4_When_CalculatingDiscount_Then_DiscountShouldBeZero()
    {
        // Arrange
        var item = new SaleItem("Test Product", "TP01", 3, 10);

        // Assert
        Assert.Equal(0, item.DiscountPercentage);
        Assert.Equal(0, item.DiscountAmount);
        Assert.Equal(30, item.TotalAmount);
    }

    [Fact]
    public void Given_QuantityBetween4And9_When_CalculatingDiscount_Then_ShouldApply10PercentDiscount()
    {
        // Arrange
        var item = new SaleItem("Test Product", "TP01", 5, 10);

        // Assert
        Assert.Equal(0.10m, item.DiscountPercentage);
        Assert.Equal(5, item.DiscountAmount);
        Assert.Equal(45, item.TotalAmount);
    }

    [Fact]
    public void Given_QuantityBetween10And20_When_CalculatingDiscount_Then_ShouldApply20PercentDiscount()
    {
        // Arrange
        var item = new SaleItem("Test Product", "TP01", 15, 10);

        // Assert
        Assert.Equal(0.20m, item.DiscountPercentage);
        Assert.Equal(30, item.DiscountAmount);
        Assert.Equal(120, item.TotalAmount);
    }

    [Fact]
    public void Given_QuantityGreaterThan20_When_CreatingItem_Then_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new SaleItem("Test Product", "TP01", 21, 10));
    }
}
