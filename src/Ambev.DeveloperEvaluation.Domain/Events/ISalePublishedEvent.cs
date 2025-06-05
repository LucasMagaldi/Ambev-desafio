namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public interface ISaleEventPublisher
    {
        void PublishSaleCreated(Guid saleId);
        void PublishSaleModified(Guid saleId);
        void PublishSaleCancelled(Guid saleId);
        void PublishItemCancelled(Guid itemId);
    }
}
