namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents the response returned after creating a sale
/// </summary>
public class CreateSaleResponse
{
    public Guid Id { get; set; }
    public string Customer { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public bool IsCancelled { get; set; }
}
