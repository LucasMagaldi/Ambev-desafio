using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResponse>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ISaleEventPublisher _eventPublisher;

    private readonly ILogger _logger;


    public CancelSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        ILogger<CancelSaleHandler> logger,
        ISaleEventPublisher eventPublisher)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
        _eventPublisher = eventPublisher;
    }

    public async Task<CancelSaleResponse> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale == null)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        sale.Cancel();
        await _saleRepository.UpdateAsync(sale, cancellationToken);

        _eventPublisher.PublishSaleCancelled(sale.Id);

        return new CancelSaleResponse
        {
            Success = true
        };
    }
}
