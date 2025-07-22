using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CancelSaleCommandValidatorTests
{
    private readonly CancelSaleCommandValidator _validator;

    public CancelSaleCommandValidatorTests()
    {
        _validator = new CancelSaleCommandValidator();
    }

    [Fact]
    public void Given_ValidCancelSaleCommand_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var command = new CancelSaleCommand(Guid.NewGuid());

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Given_EmptyGuid_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var command = new CancelSaleCommand(Guid.Empty);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}
