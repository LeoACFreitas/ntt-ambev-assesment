using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class DeleteSaleCommandValidatorTests
{
    private readonly DeleteSaleCommandValidator _validator;

    public DeleteSaleCommandValidatorTests()
    {
        _validator = new DeleteSaleCommandValidator();
    }

    [Fact]
    public void Given_ValidDeleteSaleCommand_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var command = new DeleteSaleCommand(Guid.NewGuid());

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Given_EmptyGuid_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var command = new DeleteSaleCommand(Guid.Empty);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}
