using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class GetSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleHandler _handler;

    public GetSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleHandler(_saleRepository, _mapper);
    }

    [Fact]
    public async Task Given_ExistingSaleId_When_Handled_Then_SaleShouldBeReturned()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new GetSaleCommand(saleId);
        var saleEntity = new Sale();
        var resultDto = new GetSaleResult();

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns(saleEntity);
        _mapper.Map<GetSaleResult>(saleEntity)
            .Returns(resultDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _saleRepository.Received(1).GetByIdAsync(saleId, Arg.Any<CancellationToken>());
        _mapper.Received(1).Map<GetSaleResult>(saleEntity);
        Assert.Equal(resultDto, result);
    }

    [Fact]
    public async Task Given_NonExistingSaleId_When_Handled_Then_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new GetSaleCommand(saleId);

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns((Sale)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Contains($"Sale with ID {saleId} not found", exception.Message);
        _mapper.DidNotReceive().Map<GetSaleResult>(Arg.Any<Sale>());
    }
}
