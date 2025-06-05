using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Infrastructure.Events
{
    public class LoggingSaleEventPublisher : ISaleEventPublisher
    {
        private readonly ILogger<LoggingSaleEventPublisher> _logger;

        public LoggingSaleEventPublisher(ILogger<LoggingSaleEventPublisher> logger)
        {
            _logger = logger;
        }

        public void PublishSaleCreated(Guid saleId)
        {
            _logger.LogInformation("Event: SaleCreated - Sale ID: {SaleId}", saleId);
        }

        public void PublishSaleModified(Guid saleId)
        {
            _logger.LogInformation("Event: SaleModified - Sale ID: {SaleId}", saleId);
        }

        public void PublishSaleCancelled(Guid saleId)
        {
            _logger.LogInformation("Event: SaleCancelled - Sale ID: {SaleId}", saleId);
        }

        public void PublishItemCancelled(Guid itemId)
        {
            _logger.LogInformation("Event: ItemCancelled - Item ID: {ItemId}", itemId);
        }
    }
}
