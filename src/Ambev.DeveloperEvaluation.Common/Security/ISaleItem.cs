namespace Ambev.DeveloperEvaluation.Common.Security
{
    public interface ISaleItem
    {
        Guid ProductId { get; }
        string ProductName { get; }
        int Quantity { get; }
        decimal UnitPrice { get; }
        decimal Discount { get; }
        decimal TotalAmount { get; }
    }
}