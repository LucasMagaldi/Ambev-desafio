namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;

public class GetSaleByIdResponse
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string Customer { get; set; }
    public string Branch { get; set; }
    public decimal TotalAmount { get; set; }
    public List<GetSaleItemResponse> Items { get; set; }
}

public class GetSaleItemResponse
{
    public string Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
}
