namespace InvoiceTracker.VatService.Models
{
    public class VatSummary
    {
        /// <summary>
        /// Total Output VAT from sales.
        /// </summary>
        public decimal OutputVat { get; set; }

        /// <summary>
        /// Total Claimable Input VAT from purchases.
        /// </summary>
        public decimal ClaimableInputVat { get; set; }

        /// <summary>
        /// VAT Payable = Output VAT - Claimable Input VAT.
        /// </summary>
        public decimal VatPayable { get; set; }

        /// <summary>
        /// Number of invoices requiring review.
        /// </summary>
        public int ReviewRequiredCount { get; set; }

        /// <summary>
        /// Number of non-claimable invoices.
        /// </summary>
        public int NonClaimableCount { get; set; }

        /// <summary>
        /// Note on excluded invoices.
        /// </summary>
        public string ExcludedInvoicesNote { get; set; } = string.Empty;
    }
}