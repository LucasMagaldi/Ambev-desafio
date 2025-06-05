using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;
using System;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {
        [Fact]
        public void AddItem_WithQuantity3_ShouldHaveNoDiscount()
        {
            var sale = new Sale("Lucas", "Filial A", DateTime.UtcNow);
            var quantity = 3;
            var Price = 10m;
            var discount = 0m;

            sale.AddItem(Guid.NewGuid(), "Produto X", quantity, Price, discount);

            var item = Assert.Single(sale.Items);
            Assert.Equal(discount, item.Discount);
            Assert.Equal(quantity * Price, item.TotalAmount);
        }

        [Fact]
        public void AddItem_WithQuantity10_ShouldHave20PercentDiscount()
        {
            var sale = new Sale("Lucas", "Filial B", DateTime.UtcNow);
            var quantity = 10;
            var Price = 10m;
            var discountPerUnit = Price * 0.2m;

            sale.AddItem(Guid.NewGuid(), "Produto Y", quantity, Price, discountPerUnit);

            var item = Assert.Single(sale.Items);
            Assert.Equal(discountPerUnit, item.Discount);
            Assert.Equal(quantity * (Price - discountPerUnit), item.TotalAmount); // 10 * 8 = 80
        }

    }
}