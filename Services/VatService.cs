using InvoiceTracker.VatService.Data;
using InvoiceTracker.VatService.Enums;
using InvoiceTracker.VatService.Models;

namespace InvoiceTracker.VatService.Services
{
    public class VatService : IVatService
    {
        private readonly InMemoryDataStore _dataStore; // Replace with DbContext in production

        public VatService()
        {
            _dataStore = new InMemoryDataStore(); // Initialize demo data
        }

        public async Task<IEnumerable<SalesInvoice>> GetSalesInvoicesAsync(DateTime startDate, DateTime endDate, bool includeNonFiscalized)
        {
            // Simulate async DB call
            await Task.Delay(100);

            var invoices = _dataStore.SalesInvoices
                .Where(i => i.InvoiceDate >= startDate && i.InvoiceDate <= endDate);

            if (!includeNonFiscalized)
            {
                invoices = invoices.Where(i => i.FiscalizationStatus == FiscalizationStatus.Validated);
            }

            return invoices;
        }

        public async Task<IEnumerable<PurchaseInvoice>> GetPurchaseInvoicesAsync(DateTime startDate, DateTime endDate, bool includeNonFiscalized)
        {
            // Simulate async DB call
            await Task.Delay(100);

            var invoices = _dataStore.PurchaseInvoices
                .Where(i => i.InvoiceDate >= startDate && i.InvoiceDate <= endDate);

            if (!includeNonFiscalized)
            {
                invoices = invoices.Where(i => i.FiscalizationStatus == FiscalizationStatus.Validated);
            }

            return invoices;
        }

        public async Task<VatSummary> GetVatSummaryAsync(DateTime startDate, DateTime endDate, bool includeReviewRequired, string? version)
        {
            // Version handling: For historical, could load different rules; simplified here
            if (version != null)
            {
                // Placeholder: Adjust rates or logic based on version
            }

            var sales = await GetSalesInvoicesAsync(startDate, endDate, false);
            var purchases = await GetPurchaseInvoicesAsync(startDate, endDate, false);

            decimal outputVat = sales.Sum(s => s.VatAmount);

            decimal claimableInputVat = purchases
                .Where(p => includeReviewRequired || p.ClaimableStatus != ClaimableStatus.ReviewRequired)
                .Sum(p =>
                {
                    if (p.ClaimableStatus == ClaimableStatus.Claimable)
                        return p.VatAmount;
                    if (p.ClaimableStatus == ClaimableStatus.PartiallyClaimable)
                        return p.VatAmount * (p.ClaimablePercent / 100m);
                    return 0m;
                });

            var reviewRequiredInvoices = purchases.Where(p => p.ClaimableStatus == ClaimableStatus.ReviewRequired).ToList();
            var nonClaimableInvoices = purchases.Where(p => p.ClaimableStatus == ClaimableStatus.NotClaimable).ToList();

            return new VatSummary
            {
                OutputVat = outputVat,
                ClaimableInputVat = claimableInputVat,
                VatPayable = outputVat - claimableInputVat,
                ReviewRequiredCount = reviewRequiredInvoices.Count(),
                NonClaimableCount = nonClaimableInvoices.Count(),
                ExcludedInvoicesNote = "Non-fiscalized and 'review required' (unless included) are excluded from claimable totals."
            };
        }

        public async Task<ExportVatSummary> ExportVatSummaryAsync(DateTime startDate, DateTime endDate, string format)
        {
            var summary = await GetVatSummaryAsync(startDate, endDate, false, null);
            var sales = await GetSalesInvoicesAsync(startDate, endDate, false);
            var purchases = await GetPurchaseInvoicesAsync(startDate, endDate, false);

            // In production, generate CSV/PDF/JSON file content
            string data = format switch
            {
                "csv" => "OutputVAT,ClaimableInputVAT,VatPayable\n" + $"{summary.OutputVat},{summary.ClaimableInputVat},{summary.VatPayable}",
                "json" => System.Text.Json.JsonSerializer.Serialize(summary),
                "pdf" => "PDF content placeholder", // Use iText or similar
                _ => throw new ArgumentException("Invalid format")
            };

            return new ExportVatSummary
            {
                Format = format,
                Data = data,
                SalesInvoices = sales,
                PurchaseInvoices = purchases
            };
        }
    }
}