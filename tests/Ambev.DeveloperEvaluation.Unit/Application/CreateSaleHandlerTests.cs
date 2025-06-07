using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateSaleCommandHandlerTests
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleCommandHandler> _logger;
    private readonly ISaleEventPublisher _eventPublisher;
    private readonly CreateSaleCommandHandler _handler;

    public CreateSaleCommandHandlerTests()
    {
        _repository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<CreateSaleCommandHandler>>();
        _eventPublisher = Substitute.For<ISaleEventPublisher>();
        _handler = new CreateSaleCommandHandler(_repository, _mapper, _logger, _eventPublisher);
    }

    [Fact(DisplayName = "Should create sale with no discount when quantity is less than 4")]
    public async Task Should_CreateSale_WithNoDiscount_WhenQuantityLessThanFour()
    {
        var command = new CreateSaleCommand
        {
            Customer = "Cliente A",
            Branch = "Filial X",
            Date = DateTime.UtcNow,
            Items = new List<CreateSaleItem>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Produto A",
                    Quantity = 3,
                    Price = 10m
                }
            }
        };

        _mapper.Map<CreateSaleResult>(Arg.Any<Sale>())
            .Returns(new CreateSaleResult { Id = Guid.NewGuid() });

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();

        await _repository.Received(1).AddAsync(Arg.Is<Sale>(s =>
            s.Items.Count == 1 &&
            s.Items.First().Discount == 0m &&
            s.Items.First().TotalAmount == 30m));

        _eventPublisher.Received(1).PublishSaleCreated(Arg.Any<Guid>());
    }

    [Fact(DisplayName = "Should create sale with 20% discount when quantity is 10")]
    public async Task Should_CreateSale_WithTwentyPercentDiscount_WhenQuantityIsTen()
    {
        var command = new CreateSaleCommand
        {
            Customer = "Cliente B",
            Branch = "Filial Y",
            Date = DateTime.UtcNow,
            Items = new List<CreateSaleItem>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Produto B",
                    Quantity = 10,
                    Price = 10m
                }
            }
        };

        _mapper.Map<CreateSaleResult>(Arg.Any<Sale>())
            .Returns(new CreateSaleResult { Id = Guid.NewGuid() });

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();

        await _repository.Received(1).AddAsync(Arg.Is<Sale>(s =>
            s.Items.Count == 1 &&
            s.Items.First().Discount == 2m &&
            s.Items.First().TotalAmount == 80m));

        _eventPublisher.Received(1).PublishSaleCreated(Arg.Any<Guid>());
    }
}
