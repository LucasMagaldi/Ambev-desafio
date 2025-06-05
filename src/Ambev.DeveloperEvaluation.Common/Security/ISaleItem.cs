namespace Ambev.DeveloperEvaluation.Common.Security
{
    public interface ISaleItem
    {
        Guid ProductId { get; }
        string ProductName { get; }
        int Quantity { get; }
        decimal Price { get; }
        decimal Discount { get; }
        decimal TotalAmount { get; }
    }
}