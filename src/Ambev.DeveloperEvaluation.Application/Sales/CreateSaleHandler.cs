using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Application.Dtos;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, SaleDto>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSaleCommandHandler> _logger;

        private readonly ISaleEventPublisher _eventPublisher;

        public CreateSaleCommandHandler(
            ISaleRepository repository,
            IMapper mapper,
            ILogger<CreateSaleCommandHandler> logger,
            ISaleEventPublisher eventPublisher)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _eventPublisher = eventPublisher;
        }

        public async Task<SaleDto> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = new Sale(request.Customer, request.Branch, request.Date);

            foreach (var item in request.Items)
            {
                decimal discount = 0m;

                if (item.Quantity >= 10 && item.Quantity <= 20)
                    discount = item.Price * 0.2m;
                else if (item.Quantity >= 4)
                    discount = item.Price * 0.1m;

                sale.AddItem(item.ProductId, item.ProductName, item.Quantity, item.Price, discount);
            }

            await _repository.AddAsync(sale);

            _eventPublisher.PublishSaleCreated(sale.Id);

            return _mapper.Map<SaleDto>(sale);
        }
    }
}