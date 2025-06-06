namespace Ambev.DeveloperEvaluation.Common.Security
{
    public interface ISale
    {
        Guid Id { get; }
        string Customer { get; }
        string Branch { get; }
        DateTime Date { get; }
        bool IsCancelled { get; }
        decimal TotalAmount { get; }
        IEnumerable<ISaleItem> Items { get; }
    }

}