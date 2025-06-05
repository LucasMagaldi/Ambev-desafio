using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface ISaleService
{
    Task<Sale> CreateSaleAsync(
        string customer,
        string branch,
        DateTime date,
        List<(Guid productId, string productName, int quantity, decimal Price)> items
    );

    Task<Sale?> GetByIdAsync(Guid id);

    Task<IEnumerable<Sale>> GetAllAsync();

    Task CancelSaleAsync(Guid saleId);

    Task<bool> DeleteAsync(Guid saleId);
}