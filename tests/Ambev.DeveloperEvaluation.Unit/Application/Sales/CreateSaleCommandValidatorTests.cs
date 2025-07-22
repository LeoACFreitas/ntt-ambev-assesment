using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using FluentValidation.TestHelper;
using Xunit;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CreateSaleCommandValidatorTests
{
    private readonly CreateSaleCommandValidator _validator;
    private readonly Faker<CreateSaleCommand> _faker;
    private readonly Faker<CreateSaleItemRequest> _itemFaker;

    public CreateSaleCommandValidatorTests()
    {
        _validator = new CreateSaleCommandValidator();
        _itemFaker = new Faker<CreateSaleItemRequest>()
            .RuleFor(i => i.ProductName, f => f.Commerce.ProductName())
            .RuleFor(i => i.ProductCode, f => f.Commerce.ProductAdjective().ToUpper() + f.Random.Number(1000, 9999))
            .RuleFor(i => i.Quantity, f => f.Random.Int(1, 20))
            .RuleFor(i => i.UnitPrice, f => f.Finance.Amount(1, 100));

        _faker = new Faker<CreateSaleCommand>()
            .RuleFor(c => c.SaleNumber, f => f.Random.AlphaNumeric(10))
            .RuleFor(c => c.CustomerName, f => f.Name.FullName())
            .RuleFor(c => c.CustomerEmail, f => f.Internet.Email())
            .RuleFor(c => c.CustomerDocument, f => f.Random.AlphaNumeric(11))
            .RuleFor(c => c.BranchName, f => f.Company.CompanyName())
            .RuleFor(c => c.BranchCode, f => f.Random.AlphaNumeric(5))
            .RuleFor(c => c.SaleDate, f => f.Date.Past())
            .RuleFor(c => c.Items, f => _itemFaker.Generate(1));
    }

    [Fact]
    public void Given_ValidCreateSaleCommand_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var command = _faker.Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]
    [InlineData("too_long_sale_number_that_exceeds_the_maximum_length_of_50_characters")]
    public void Given_InvalidSaleNumber_When_Validated_Then_ShouldHaveError(string saleNumber)
    {
        // Arrange
        var command = _faker.Generate();
        command.SaleNumber = saleNumber;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SaleNumber);
    }

    [Theory]
    [InlineData("invalid-email")]
    [InlineData("test@")]
    [InlineData("@domain.com")]
    public void Given_InvalidCustomerEmail_When_Validated_Then_ShouldHaveError(string email)
    {
        // Arrange
        var command = _faker.Generate();
        command.CustomerEmail = email;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerEmail);
    }

    [Fact]
    public void Given_EmptyItemsList_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var command = _faker.Generate();
        command.Items = new List<CreateSaleItemRequest>();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Items);
    }

    [Fact]
    public void Given_InvalidSaleItemRequest_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var command = _faker.Generate();
        command.Items = new List<CreateSaleItemRequest>
        {
            new CreateSaleItemRequest { ProductName = "", ProductCode = "PC01", Quantity = 1, UnitPrice = 10 }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor("Items[0].ProductName");
    }
}
