
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    [Fact]
    public void Given_NewSale_When_AddingValidItem_Then_ItemShouldBeAddedAndTotalRecalculated()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale().Generate();
        var item = SaleItemTestData.GenerateValidSaleItem().Generate();

        // Act
        sale.AddItem(item.ProductName, item.ProductCode, item.Quantity, item.UnitPrice);

        // Assert
        Assert.Single(sale.Items);
        Assert.Equal(item.TotalAmount, sale.TotalAmount);
    }

    [Fact]
    public void Given_NewSale_When_AddingInvalidItem_Then_ShouldThrowArgumentException()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale().Generate();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => sale.AddItem("", "", 0, 0));
    }

    [Fact]
    public void Given_SaleWithItem_When_RemovingItem_Then_ItemShouldBeRemovedAndTotalRecalculated()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale().Generate();
        var item = SaleItemTestData.GenerateValidSaleItem().Generate();
        sale.AddItem(item.ProductName, item.ProductCode, item.Quantity, item.UnitPrice);

        // Act
        sale.RemoveItem(item.ProductCode);

        // Assert
        Assert.Empty(sale.Items);
        Assert.Equal(0, sale.TotalAmount);
    }

    [Fact]
    public void Given_SaleWithItem_When_UpdatingItemQuantity_Then_QuantityShouldBeUpdatedAndTotalRecalculated()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale().Generate();
        var item = SaleItemTestData.GenerateValidSaleItem().Generate();
        sale.AddItem(item.ProductName, item.ProductCode, item.Quantity, item.UnitPrice);
        var newQuantity = item.Quantity + 1;

        // Act
        sale.UpdateItemQuantity(item.ProductCode, newQuantity);

        // Assert
        var updatedItem = sale.Items.First();
        Assert.Equal(newQuantity, updatedItem.Quantity);
        Assert.NotEqual(item.TotalAmount, sale.TotalAmount);
    }

    [Fact]
    public void Given_ActiveSale_When_Cancelling_Then_ShouldBeCancelled()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale().Generate();

        // Act
        sale.Cancel();

        // Assert
        Assert.True(sale.IsCancelled);
    }

    [Fact]
    public void Given_CancelledSale_When_AddingItem_Then_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale().Generate();
        sale.Cancel();
        var item = SaleItemTestData.GenerateValidSaleItem().Generate();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => sale.AddItem(item.ProductName, item.ProductCode, item.Quantity, item.UnitPrice));
    }
}
