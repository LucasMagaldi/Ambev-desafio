using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Representa os dados de retorno de uma venda.
/// </summary>
public class GetSaleResult
{
    public Guid Id { get; set; }
    public string Customer { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public bool IsCancelled { get; set; }
    public decimal TotalAmount { get; set; }
    public List<GetSaleItemResult> Items { get; set; } = new();
}

public class GetSaleItemResult
{
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
}
