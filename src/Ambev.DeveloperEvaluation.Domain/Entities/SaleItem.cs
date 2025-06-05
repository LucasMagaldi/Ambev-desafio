using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity, ISaleItem
    {
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; } = string.Empty;
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalAmount => (Price - Discount) * Quantity;

        public Guid SaleId { get; set; }

        protected SaleItem() { }

        public SaleItem(Guid productId, string productName, int quantity, decimal price, decimal discount)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
            Discount = discount;
        }
    }
}