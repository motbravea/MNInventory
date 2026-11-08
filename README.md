# MNInventory

MNInventory is a C# desktop application for counting retail inventory and reconciling physical counts against expected stock. It combines a keyboard-friendly Windows Forms interface, shared MySQL storage, Excel import/export, scan history, and printable barcode labels.

The repository includes the complete application source and a fictional sample dataset for local evaluation.

## What it does

- **Count by barcode:** scan or type a barcode, add a quantity, or enable Quick Scan to add one unit per Enter key.
- **Reconcile stock:** view counted quantities and adjustments, filter missing/extra/balanced products, and find eligible products that have not been scanned.
- **Update the catalog:** import a source workbook while preserving existing counts; add a product manually when needed.
- **Review activity:** search and filter scan history, inspect computer names, undo scans, and export history as CSV.
- **Exchange reports:** export counted inventory and unscanned products to Excel; restore counts from a previous counted-inventory workbook.
- **Print labels:** preview Code 128 labels and export selected products to PDF or PNG pages in A4 or Letter layouts.

`Adjustment = Counted - System Inventory`: a negative result indicates missing units; a positive result indicates extra units.

## Technology and structure

| Component | Implementation |
| --- | --- |
| Desktop interface | C#, .NET 10, Windows Forms |
| Shared persistence | MySQL with parameterized commands through MySql.Data |
| Workbook processing | ClosedXML |
| Barcode rendering | ZXing.Net, Code 128 |
| PDF output | PdfSharpCore |
| Standalone count tool | Console entry point sharing the inventory and Excel services |

```text
MNInventory.slnx
src/
  MNInventoryApp/    Models, inventory rules, Excel and MySQL services, CLI
  MNInventoryUI/     Forms, theme, barcode previews and report exports
database/
  schema.sql        Empty application schema
  sample-data.sql   Fictional products for a local demonstration
docs/               Setup, workbook formats, and architecture
```

The desktop project references the core project. Both are included in the root solution. [Architecture and behavior](docs/architecture.md) describes the data flow and count lifecycle.

## Run the desktop app

Prerequisites: Windows, the **.NET 10 SDK**, and a reachable MySQL server. Visual Studio is optional; use a version that supports .NET 10 if you prefer the IDE.

1. Create the database with [database/schema.sql](database/schema.sql). Optionally load [database/sample-data.sql](database/sample-data.sql) to try fictional products.
2. Configure a MySQL account for the application and provide its connection string through `MNINVENTORY_CONNECTION_STRING`.
3. From the repository root, restore, build, and launch:

```powershell
# This prompt keeps a literal credential out of command history.
$env:MNINVENTORY_CONNECTION_STRING = Read-Host "Enter your local MySQL connection string"

dotnet restore MNInventory.slnx
dotnet build MNInventory.slnx -c Release --no-restore
dotnet run --project src/MNInventoryUI/MNInventoryUI.csproj -c Release --no-build
```

Use a connection string with your own `Server`, `Port`, `Database=mninventory`, `Uid`, and `Pwd` values. The app reads the environment variable; it does not load `.env` files. A connected empty database opens an empty interface so you can choose **Import New Inventory**.

See [the setup guide](docs/setup.md) for database commands, connection details, and troubleshooting.

## Try a count

1. Load the fictional SQL sample data, or import your own local workbook with the [documented headers](docs/workbook-format.md).
2. With the SQL sample loaded, enter `DEMO0001` for Demo Notebook. In normal mode, press Enter, enter `3`, then press Enter again. Quick Scan adds one unit immediately.
3. On a fresh sample session, Demo Notebook now shows counted `3`, expected `12`, and adjustment `-9`. Use **Ctrl+Z** to undo the latest scan from the current app session.
4. Open the discrepancy and unscanned views, inspect history, then export the counted workbook with **Ctrl+E**.
5. Select products in the barcode-label window, preview a label, and save a PDF or PNG page.

**Import New Inventory** preserves counts for matching products. **Import Scanned Excel** replaces current counts and resets history. **Start New Count** clears counts and history for the shared database while retaining the source catalog. Export anything you want to retain before either reset operation.

## Run the workbook-only CLI

The console tool uses Excel files and does not require MySQL:

```powershell
dotnet run --project src/MNInventoryApp/MNInventoryApp.csproj -- --help
dotnet run --project src/MNInventoryApp/MNInventoryApp.csproj -- "C:\path\source.xlsx" "C:\path\counts.xlsx"
```

The source workbook supplies expected stock. The count workbook is created or updated as you scan products and add quantities. See [workbook formats](docs/workbook-format.md).

## Local data

Use a local demo database when evaluating the project. The supplied SQL records are fictional. Credentials, database dumps, workbooks, generated PDF/CSV reports, build output, and local editor files are excluded by `.gitignore`. Keep real inventory and exported reports outside the source tree; ignore rules do not remove files already committed.

## Validation and current limits

Both application projects compile. Existing compiler nullability warnings remain, and the current PdfSharpCore dependency chain produces NuGet security advisories for its transitive ImageSharp package.

Synthetic smoke checks passed for workbook header detection, barcode aliases and leading zeroes, conflicting barcodes, count adjustments, workbook export/reload, unscanned filtering, invalid workbook input, and missing/provided connection configuration. The workbook-only CLI also saved a synthetic count through its interactive scan flow.

Live MySQL integration, physical barcode scanners, and printers have not been tested in this validation pass. These remain environment-specific checks before operational use.
