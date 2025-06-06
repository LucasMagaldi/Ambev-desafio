using MediatR;
using System;
using System.Collections.Generic;



namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public string Customer { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public List<CreateSaleItem> Items { get; set; } = new();
    }

    public class CreateSaleItem
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}