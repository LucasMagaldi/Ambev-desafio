using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleResponse
{
    public Guid Id { get; set; }
    public string Customer { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public bool IsCancelled { get; set; }
    public decimal TotalAmount { get; set; }
    public List<GetSalesItemResponse> Items { get; set; } = new();
}

public class GetSalesItemResponse
{
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
}
