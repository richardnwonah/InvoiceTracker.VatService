using InvoiceTracker.VatService.Models;

namespace InvoiceTracker.VatService.Services
{
    public interface IVatService
    {
        Task<IEnumerable<SalesInvoice>> GetSalesInvoicesAsync(DateTime startDate, DateTime endDate, bool includeNonFiscalized);
        Task<IEnumerable<PurchaseInvoice>> GetPurchaseInvoicesAsync(DateTime startDate, DateTime endDate, bool includeNonFiscalized);
        Task<VatSummary> GetVatSummaryAsync(DateTime startDate, DateTime endDate, bool includeReviewRequired, string? version);
        Task<ExportVatSummary> ExportVatSummaryAsync(DateTime startDate, DateTime endDate, string format);
    }
}