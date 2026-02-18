using InvoiceTracker.VatService.Enums;

namespace InvoiceTracker.VatService.Models
{
    public abstract class Invoice
    {
        public string LineItem { get; set; } = string.Empty;
        public decimal NetAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal PayableTotal { get; set; }
        public VatCategory VatCategory { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Irn { get; set; } = string.Empty;
        public string QrCodeReference { get; set; } = string.Empty;
        public FiscalizationStatus FiscalizationStatus { get; set; }
    }

    public class SalesInvoice : Invoice
    {
        public string PaymentStatus { get; set; } = string.Empty; // Paid / Cancelled
    }

    public class PurchaseInvoice : Invoice
    {
        public string SupplierInformation { get; set; } = string.Empty;
        public ClaimableStatus ClaimableStatus { get; set; }
        public decimal ClaimablePercent { get; set; } // If partially claimable
        public ReasonCode? ReasonCode { get; set; } // If not claimable or review required
    }
}