using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Sale> AddAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}