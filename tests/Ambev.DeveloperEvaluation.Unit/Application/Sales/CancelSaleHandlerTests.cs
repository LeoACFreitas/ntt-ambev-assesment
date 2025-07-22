using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CancelSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly CancelSaleHandler _handler;

    public CancelSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CancelSaleHandler(_saleRepository, _mapper);
    }

    [Fact]
    public async Task Given_ExistingActiveSaleId_When_Handled_Then_SaleShouldBeCancelledAndUpdated()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new CancelSaleCommand(saleId);
        var saleEntity = new Sale();
        var resultDto = new CancelSaleResult();

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns(saleEntity);
        _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(saleEntity);
        _mapper.Map<CancelSaleResult>(saleEntity)
            .Returns(resultDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _saleRepository.Received(1).GetByIdAsync(saleId, Arg.Any<CancellationToken>());
        await _saleRepository.Received(1).UpdateAsync(Arg.Is<Sale>(s => s.IsCancelled), Arg.Any<CancellationToken>());
        _mapper.Received(1).Map<CancelSaleResult>(saleEntity);
        Assert.Equal(resultDto, result);
    }

    [Fact]
    public async Task Given_NonExistingSaleId_When_Handled_Then_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new CancelSaleCommand(saleId);

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns((Sale)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Contains($"Sale with ID {saleId} not found", exception.Message);
        await _saleRepository.DidNotReceive().UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Given_AlreadyCancelledSaleId_When_Handled_Then_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new CancelSaleCommand(saleId);
        var saleEntity = new Sale();
        var saleType = typeof(Sale);
        saleType.GetProperty("IsCancelled")?.SetValue(saleEntity, true);

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns(saleEntity);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Contains("Sale is already cancelled", exception.Message);
        await _saleRepository.DidNotReceive().UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }
}
