using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISaleEventPublisher _eventPublisher;

    public UpdateSaleHandler(
        ISaleRepository repository,
        IMapper mapper,
        ISaleEventPublisher eventPublisher)
    {
        _repository = repository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new FluentValidation.ValidationException(validationResult.Errors);

        var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null)
            throw new KeyNotFoundException($"Sale {request.Id} not found");

        sale.Update(request.Customer, request.Branch, request.Date);
        sale.ClearItems();

        foreach (var item in request.Items)
        {
            decimal discount = 0;
            if (item.Quantity >= 10 && item.Quantity <= 20)
                discount = item.Price * 0.2m;
            else if (item.Quantity >= 4)
                discount = item.Price * 0.1m;

            sale.AddItem(item.ProductId, item.ProductName, item.Quantity, item.Price, discount);
        }

        await _repository.UpdateAsync(sale, cancellationToken);
        _eventPublisher.PublishSaleModified(sale.Id);

        return new UpdateSaleResult { Success = true };
    }

}
