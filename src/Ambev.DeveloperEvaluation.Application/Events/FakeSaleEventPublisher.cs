using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Events;

public class FakeSaleEventPublisher : ISaleEventPublisher
{
    private readonly ILogger<FakeSaleEventPublisher> _logger;

    public FakeSaleEventPublisher(ILogger<FakeSaleEventPublisher> logger)
    {
        _logger = logger;
    }

    public void PublishSaleCreated(Guid saleId)
    {
        _logger.LogInformation("📢 Event simulated: SaleCreated - {SaleId}", saleId);
    }

    public void PublishSaleModified(Guid saleId)
    {
        _logger.LogInformation("📢 Event simulated: SaleModified - {SaleId}", saleId);
    }

    public void PublishSaleCancelled(Guid saleId)
    {
        _logger.LogInformation("📢 Event simulated: SaleCancelled - {SaleId}", saleId);
    }

    public void PublishItemCancelled(Guid itemId)
    {
        _logger.LogInformation("📢 Event simulated: ItemCancelled - {ItemId}", itemId);
    }
}
