using MediatR;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Comando para obter todas as vendas.
/// </summary>
public class GetSaleCommand : IRequest<List<GetSaleResult>>
{
}
