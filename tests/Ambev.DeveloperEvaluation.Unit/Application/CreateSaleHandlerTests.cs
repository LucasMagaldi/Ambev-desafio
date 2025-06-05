using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Dtos;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleCommandHandler"/> class.
/// </summary>
public class CreateSaleCommandHandlerTests
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleCommandHandler> _logger;
    private readonly CreateSaleCommandHandler _handler;

    public CreateSaleCommandHandlerTests()
    {
        _repository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<CreateSaleCommandHandler>>();
        _handler = new CreateSaleCommandHandler(_repository, _mapper, _logger);
    }

    /// <summary>
    /// Tests that a valid sale creation request with no discount is processed correctly.
    /// </summary>
    [Fact(DisplayName = "Given quantity 3 When creating sale Then no discount applied")]
    public async Task Handle_Quantity3_NoDiscount()
    {
        var command = new CreateSaleCommand
        {
            Customer = "Cliente A",
            Branch = "Filial X",
            Date = DateTime.UtcNow,
            Items = new List<CreateSaleItemDto>
        {
            new CreateSaleItemDto
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Produto A",
                Quantity = 3,
                UnitPrice = 10m
            }
        }
        };

        var sale = new Sale(command.Customer, command.Branch, command.Date);
        _mapper.Map<SaleDto>(Arg.Any<Sale>()).Returns(new SaleDto { Id = sale.Id });

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        await _repository.Received(1).AddAsync(Arg.Is<Sale>(s =>
            s.Items.Count == 1 &&
            s.Items.First().Discount == 0m &&
            s.Items.First().TotalAmount == 30m));
    }

    [Fact(DisplayName = "Given quantity 10 When creating sale Then 20 percent discount applied")]
    public async Task Handle_Quantity10_Apply20PercentDiscount()
    {
        var command = new CreateSaleCommand
        {
            Customer = "Cliente B",
            Branch = "Filial Y",
            Date = DateTime.UtcNow,
            Items = new List<CreateSaleItemDto>
        {
            new CreateSaleItemDto
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Produto B",
                Quantity = 10,
                UnitPrice = 10m
            }
        }
        };

        var sale = new Sale(command.Customer, command.Branch, command.Date);
        _mapper.Map<SaleDto>(Arg.Any<Sale>()).Returns(new SaleDto { Id = sale.Id });

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        await _repository.Received(1).AddAsync(Arg.Is<Sale>(s =>
            s.Items.Count == 1 &&
            s.Items.First().Discount == 2m &&
            s.Items.First().TotalAmount == 80m));
    }
}