# InvoiceTracker.VatService

**ASP.NET Core Web API** for calculating VAT payable / claimable input VAT according to Nigeria Revenue Service (NRS) e-invoicing and fiscalization rules.

This service acts as a **read-only VAT aggregation & reporting layer** on top of Invoice Tracker's AR (Accounts Receivable) and AP (Accounts Payable) data.  
It helps VAT-registered businesses quickly understand their VAT position for any selected reporting period.

**Important principles baked into the design:**

- Only **NRS-validated (fiscalized)** invoices are included in calculations  
- Users **cannot override** VAT rates or classifications  
- "Review Required" input VAT is **excluded by default** from claimable totals  
- Supports historical consistency (via version parameter – extensible for rule changes)  
- Designed for fast responses (< 2 minutes user experience goal)

## Features

- Retrieve filtered sales (AR) and purchase (AP) invoices  
- Get pre-aggregated VAT summary (Output VAT – Claimable Input VAT = VAT Payable)  
- Export-ready VAT data (JSON / CSV / PDF placeholder)  
- Clear exclusion notes for non-fiscalized, rejected, cancelled, or review-required invoices  
- Mixed VAT categories (Standard 7.5%, Zero-rated 0%, Exempt) supported on same invoice/period  
- Business logic enforces NRS compliance rules

## Tech Stack

- .NET 8 (or .NET 9 when stable)  
- ASP.NET Core Web API (minimal hosting model)  
- Swagger / OpenAPI documentation  
- Dependency Injection  
- In-memory data store (demo) → easily replaceable with EF Core / SQL Server / PostgreSQL  
- Async / await pattern throughout

## API Endpoints

| Method | Endpoint                                | Description                                      | Key Query Params                              |
|--------|-----------------------------------------|--------------------------------------------------|-----------------------------------------------|
| GET    | `/api/vat/sales-invoices`               | List sales invoices for period                   | `startDate`, `endDate`, `includeNonFiscalized` (bool, default false) |
| GET    | `/api/vat/purchase-invoices`            | List purchase invoices for period                | `startDate`, `endDate`, `includeNonFiscalized` (bool, default false) |
| GET    | `/api/vat/summary`                      | Aggregated VAT summary (main calculator view)    | `startDate`, `endDate`, `includeReviewRequired` (bool, default false), `version` (string, optional) |
| GET    | `/api/vat/export`                       | Export-ready VAT data                            | `startDate`, `endDate`, `format` (json/csv/pdf) |

**Base URL examples**  
`https://localhost:7123/api/vat/summary?startDate=2026-01-01&endDate=2026-03-31`

## Getting Started

### Prerequisites

- .NET 8 SDK (or later)

### Clone & Run

```bash
git clone <your-repo-url>
cd InvoiceTracker.VatService

# Restore packages
dotnet restore

# Build
dotnet build

# Run (development mode)
dotnet run