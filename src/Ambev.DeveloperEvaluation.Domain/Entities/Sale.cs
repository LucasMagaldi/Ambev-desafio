using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;

public class Sale : BaseEntity, ISale
{
    private readonly List<SaleItem> _items = new();

    public string Customer { get; private set; } = string.Empty;
    public string Branch { get; private set; } = string.Empty;
    public DateTime Date { get; private set; }
    public bool IsCancelled { get; private set; }
    public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

    IEnumerable<ISaleItem> ISale.Items => _items;

    public decimal TotalAmount => CalculateTotalAmount();

    public Sale(string customer, string branch, DateTime date)
    {
        Customer = customer;
        Branch = branch;
        Date = date;
    }

    public void AddItem(Guid productId, string productName, int quantity, decimal price, decimal discount)
    {
        var total = quantity * price;
        var discountAmount = total * discount;
        var totalWithDiscount = total - discountAmount;

        var item = new SaleItem(productId, productName, quantity, price, discount);
        _items.Add(item);
    }

    public void Update(string customer, string branch, DateTime date)
    {
        Customer = customer;
        Branch = branch;
        Date = date;
    }

    public void ClearItems()
    {
        _items.Clear();
    }

    public void Cancel()
    {
        if (IsCancelled) return;
        IsCancelled = true;
    }

    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }

    private decimal CalculateTotalAmount()
    {
        return _items.Sum(item => item.TotalAmount);
    }
}
