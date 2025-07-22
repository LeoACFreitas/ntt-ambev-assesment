using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class GetSaleCommandValidatorTests
{
    private readonly GetSaleCommandValidator _validator;

    public GetSaleCommandValidatorTests()
    {
        _validator = new GetSaleCommandValidator();
    }

    [Fact]
    public void Given_ValidGetSaleCommand_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var command = new GetSaleCommand(Guid.NewGuid());

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Given_EmptyGuid_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var command = new GetSaleCommand(Guid.Empty);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}
