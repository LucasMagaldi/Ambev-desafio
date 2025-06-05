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
            var unitPrice = 10m;
            var discount = 0m;

            sale.AddItem(Guid.NewGuid(), "Produto X", quantity, unitPrice, discount);

            var item = Assert.Single(sale.Items);
            Assert.Equal(discount, item.Discount);
            Assert.Equal(quantity * unitPrice, item.TotalAmount);
        }

        [Fact]
        public void AddItem_WithQuantity10_ShouldHave20PercentDiscount()
        {
            var sale = new Sale("Lucas", "Filial B", DateTime.UtcNow);
            var quantity = 10;
            var unitPrice = 10m;
            var discountPerUnit = unitPrice * 0.2m;

            sale.AddItem(Guid.NewGuid(), "Produto Y", quantity, unitPrice, discountPerUnit);

            var item = Assert.Single(sale.Items);
            Assert.Equal(discountPerUnit, item.Discount);
            Assert.Equal(quantity * (unitPrice - discountPerUnit), item.TotalAmount); // 10 * 8 = 80
        }

    }
}