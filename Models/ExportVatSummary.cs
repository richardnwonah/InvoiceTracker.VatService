namespace InvoiceTracker.VatService.Models
{
    public class ExportVatSummary
    {
        public string Format { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty; // Base64 or raw content
        public IEnumerable<SalesInvoice> SalesInvoices { get; set; } = new List<SalesInvoice>();
        public IEnumerable<PurchaseInvoice> PurchaseInvoices { get; set; } = new List<PurchaseInvoice>();
    }
}