using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

public class UpdateSaleHandlerTests
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISaleEventPublisher _eventPublisher;
    private readonly UpdateSaleHandler _handler;

    public UpdateSaleHandlerTests()
    {
        _repository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _eventPublisher = Substitute.For<ISaleEventPublisher>();
        _handler = new UpdateSaleHandler(_repository, _mapper, _eventPublisher);
    }

    [Fact(DisplayName = "Should update sale and apply correct discounts")]
    public async Task Should_UpdateSale_AndApplyDiscounts()
    {
        var saleId = Guid.NewGuid();
        var existingSale = new Sale("Old Customer", "Old Branch", DateTime.UtcNow);

        // Força o ID da entidade Sale para o mesmo do comando
        existingSale.GetType()
            .GetProperty("Id")!
            .SetValue(existingSale, saleId);

        _repository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(existingSale);

        var command = new UpdateSaleCommand
        {
            Id = saleId,
            Customer = "Updated Customer",
            Branch = "Updated Branch",
            Date = DateTime.UtcNow,
            Items = new List<UpdateSaleItemDto>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Item A",
                    Quantity = 10,
                    Price = 50m
                }
            }
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Success.Should().BeTrue();

        await _repository.Received(1).UpdateAsync(existingSale, Arg.Any<CancellationToken>());
        _eventPublisher.Received(1).PublishSaleModified(saleId);
    }

    [Fact(DisplayName = "Should throw if sale not found")]
    public async Task Should_Throw_WhenSaleNotFound()
    {
        var command = new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            Customer = "Customer",
            Branch = "Branch",
            Date = DateTime.UtcNow,
            Items = new List<UpdateSaleItemDto>()
        };

        _repository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Sale?)null);

        var act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Sale {command.Id} not found");
    }
}
