namespace Ambev.DeveloperEvaluation.Common.SecurityAdd
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