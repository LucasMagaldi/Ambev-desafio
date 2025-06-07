using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleCommand : IRequest<CancelSaleResponse>
{
    public Guid Id { get; set; }
}
