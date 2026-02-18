using InvoiceTracker.VatService.Models;
using InvoiceTracker.VatService.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceTracker.VatService.Controllers
{
    [ApiController]
    [Route("api/vat")]
    public class VatController : ControllerBase
    {
        private readonly IVatService _vatService;
        private readonly ILogger<VatController> _logger;

        public VatController(IVatService vatService, ILogger<VatController> logger)
        {
            _vatService = vatService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves sales invoices for a given period.
        /// </summary>
        /// <param name="startDate">Start of the reporting period (YYYY-MM-DD).</param>
        /// <param name="endDate">End of the reporting period (YYYY-MM-DD).</param>
        /// <param name="includeNonFiscalized">Optional: Include non-fiscalized invoices (default: false).</param>
        /// <returns>List of sales invoices.</returns>
        [HttpGet("sales-invoices")]
        public async Task<ActionResult<IEnumerable<SalesInvoice>>> GetSalesInvoices(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] bool includeNonFiscalized = false)
        {
            try
            {
                var invoices = await _vatService.GetSalesInvoicesAsync(startDate, endDate, includeNonFiscalized);
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales invoices.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves purchase invoices for a given period.
        /// </summary>
        /// <param name="startDate">Start of the reporting period (YYYY-MM-DD).</param>
        /// <param name="endDate">End of the reporting period (YYYY-MM-DD).</param>
        /// <param name="includeNonFiscalized">Optional: Include non-fiscalized invoices (default: false).</param>
        /// <returns>List of purchase invoices.</returns>
        [HttpGet("purchase-invoices")]
        public async Task<ActionResult<IEnumerable<PurchaseInvoice>>> GetPurchaseInvoices(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] bool includeNonFiscalized = false)
        {
            try
            {
                var invoices = await _vatService.GetPurchaseInvoicesAsync(startDate, endDate, includeNonFiscalized);
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving purchase invoices.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves pre-aggregated VAT summary for a given period.
        /// </summary>
        /// <param name="startDate">Start of the reporting period (YYYY-MM-DD).</param>
        /// <param name="endDate">End of the reporting period (YYYY-MM-DD).</param>
        /// <param name="includeReviewRequired">Optional: Include 'review required' in claimable totals (default: false).</param>
        /// <param name="version">Optional: VAT rule version for historical queries (default: current).</param>
        /// <returns>VAT summary.</returns>
        [HttpGet("summary")]
        public async Task<ActionResult<VatSummary>> GetVatSummary(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] bool includeReviewRequired = false,
            [FromQuery] string? version = null)
        {
            try
            {
                var summary = await _vatService.GetVatSummaryAsync(startDate, endDate, includeReviewRequired, version);
                return Ok(summary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving VAT summary.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Exports VAT summary data for a given period.
        /// </summary>
        /// <param name="startDate">Start of the reporting period (YYYY-MM-DD).</param>
        /// <param name="endDate">End of the reporting period (YYYY-MM-DD).</param>
        /// <param name="format">Export format (csv, json, pdf).</param>
        /// <returns>Export-ready data.</returns>
        [HttpGet("export")]
        public async Task<ActionResult<ExportVatSummary>> ExportVatSummary(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] string format = "json")
        {
            try
            {
                var export = await _vatService.ExportVatSummaryAsync(startDate, endDate, format);
                return Ok(export); // In production, return File() for downloads.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting VAT summary.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}