using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<SaleService> _logger;


    public SaleService(ISaleRepository saleRepository, IMapper mapper, ILogger<SaleService> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Sale> CreateSaleAsync(
        string customer,
        string branch,
        DateTime date,
        List<(Guid productId, string productName, int quantity, decimal Price)> items)
    {
        var sale = new Sale(customer, branch, date);

        foreach (var item in items)
        {
            if (item.quantity > 20)
            {
                throw new InvalidOperationException("Cannot sell more than 20 units of the same product");
            }

            decimal discount = 0;

            if (item.quantity >= 10)
            {
                discount = 0.2m; // 20%
            }
            else if (item.quantity >= 4)
            {
                discount = 0.1m; // 10%
            }

            sale.AddItem(item.productId, item.productName, item.quantity, item.Price, discount);
        }

        var validation = sale.Validate();
        if (!validation.IsValid)
        {
            throw new ArgumentException("Invalid sale data");
        }

        await _saleRepository.AddAsync(sale);
        _logger.LogInformation("Event: SaleCreated - Sale ID: {SaleId}", sale.Id);

        return sale;
    }


    public async Task<Sale?> GetByIdAsync(Guid id)
    {
        return await _saleRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _saleRepository.GetAllAsync();
    }

    public async Task<bool> DeleteAsync(Guid saleId)
    {
        var existing = await _saleRepository.GetByIdAsync(saleId);
        if (existing == null) return false;

        await _saleRepository.DeleteAsync(saleId);
        return true;
    }


    public async Task CancelSaleAsync(Guid id)
    {
        var sale = await _saleRepository.GetByIdAsync(id);
        if (sale == null) throw new InvalidOperationException("Sale not found");

        sale.Cancel();
        _logger.LogInformation("Event: SaleCancelled - Sale ID: {SaleId}", sale.Id);

        await _saleRepository.UpdateAsync(sale);
    }
}