using InvoiceTracker.VatService.Enums;
using InvoiceTracker.VatService.Models;

namespace InvoiceTracker.VatService.Data
{
    public class InMemoryDataStore
    {
        public List<SalesInvoice> SalesInvoices { get; } = new()
        {
            new SalesInvoice
            {
                LineItem = "Consulting Services",
                NetAmount = 1000m,
                VatAmount = 75m, // 7.5%
                PayableTotal = 1075m,
                VatCategory = VatCategory.StandardRated,
                InvoiceDate = DateTime.Now.AddDays(-10),
                Irn = "IRN123",
                QrCodeReference = "QR123",
                FiscalizationStatus = FiscalizationStatus.Validated,
                PaymentStatus = "Paid"
            },
            // Add more demo data as needed
        };

        public List<PurchaseInvoice> PurchaseInvoices { get; } = new()
        {
            new PurchaseInvoice
            {
                LineItem = "Electronics",
                NetAmount = 500m,
                VatAmount = 37.5m,
                PayableTotal = 537.5m,
                VatCategory = VatCategory.StandardRated,
                InvoiceDate = DateTime.Now.AddDays(-5),
                Irn = "IRN456",
                QrCodeReference = "QR456",
                FiscalizationStatus = FiscalizationStatus.Validated,
                SupplierInformation = "Supplier XYZ",
                ClaimableStatus = ClaimableStatus.Claimable,
                ClaimablePercent = 100m
            },
            new PurchaseInvoice
            {
                LineItem = "Insurance",
                NetAmount = 200m,
                VatAmount = 0m,
                PayableTotal = 200m,
                VatCategory = VatCategory.Exempt,
                InvoiceDate = DateTime.Now.AddDays(-15),
                Irn = "IRN789",
                QrCodeReference = "QR789",
                FiscalizationStatus = FiscalizationStatus.Validated,
                SupplierInformation = "Supplier ABC",
                ClaimableStatus = ClaimableStatus.NotClaimable,
                ReasonCode = ReasonCode.ExemptActivity
            },
            // Add more demo data as needed
        };
    }
}