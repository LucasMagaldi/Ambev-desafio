using System;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdResult
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string Customer { get; set; }
    public string Branch { get; set; }
    public decimal TotalAmount { get; set; }
    public List<GetSaleItemResult> Items { get; set; } = new();
}
