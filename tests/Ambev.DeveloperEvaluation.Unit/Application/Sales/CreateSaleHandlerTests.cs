using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using NSubstitute;
using Xunit;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;
    private readonly Faker<CreateSaleCommand> _faker;
    private readonly Faker<CreateSaleItemRequest> _itemFaker;

    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_saleRepository, _mapper);

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
    public async Task Given_ValidCreateSaleCommand_When_Handled_Then_SaleShouldBeCreatedAndReturned()
    {
        // Arrange
        var command = _faker.Generate();
        var saleEntity = new Sale(); // Use reflection to set properties if needed for testing
        var resultDto = new CreateSaleResult();

        _saleRepository.GetBySaleNumberAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns((Sale)null);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(saleEntity);
        _mapper.Map<CreateSaleResult>(Arg.Any<Sale>())
            .Returns(resultDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _saleRepository.Received(1).GetBySaleNumberAsync(command.SaleNumber, Arg.Any<CancellationToken>());
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        _mapper.Received(1).Map<CreateSaleResult>(Arg.Any<Sale>());
        Assert.Equal(resultDto, result);
    }

    [Fact]
    public async Task Given_ExistingSaleNumber_When_Handled_Then_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var command = _faker.Generate();
        _saleRepository.GetBySaleNumberAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(new Sale()); // Simulate existing sale

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Contains($"Sale with number {command.SaleNumber} already exists", exception.Message);
        await _saleRepository.DidNotReceive().CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Given_InvalidSaleCommand_When_Handled_Then_ShouldThrowValidationException()
    {
        // Arrange
        var command = _faker.Generate();
        command.SaleNumber = ""; // Invalid sale number

        // Act & Assert
        await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        await _saleRepository.DidNotReceive().GetBySaleNumberAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
        await _saleRepository.DidNotReceive().CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }
}
