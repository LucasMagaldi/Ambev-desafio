namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a new sale
/// </summary>
public class CreateSaleRequest
{
    public string Customer { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public List<CreateSaleItemRequest> Items { get; set; } = [];
}

public class CreateSaleItemRequest
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}