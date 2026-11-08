# Workbook formats

MNInventory uses `.xlsx` workbooks. Preserve SKU and barcode columns as **text** so spreadsheet software does not remove leading zeroes or reformat long identifiers.

## Source inventory

The importer reads the first worksheet and locates a header row within its first ten used rows. Include these named columns:

| Header | Content | Fictional example |
| --- | --- | --- |
| `SKU` | Product identifier used for catalog synchronization. | `DEMO-001` |
| `Name` | Display name. | `Demo Notebook` |
| `Barcode` | Barcode text; `/` may separate aliases. `QR` is also accepted as the header. | `DEMO0001/DEMOALT0001` |
| `System Inventory` | Expected stock as a whole number. | `12` |

Fill all product identity fields. Rows missing a SKU, name, or barcode are skipped. Blank expected stock becomes zero; a nonblank value that is not a whole number causes an import error. Keep one catalog product per SKU and avoid assigning the same barcode to different products. Headers are case-insensitive and ignore spaces/punctuation. The header-based layout does not require a particular worksheet name or fixed column order.

In the desktop app, **Import New Inventory** synchronizes source products while preserving existing counts. It matches SKU first, then falls back to a shared barcode alias. This can change the adjustment because expected stock may change while the counted total stays the same.

## Counted inventory

The count-workbook reader uses the first worksheet, skips its first row, and reads columns by position:

| Position | Exported header | Meaning |
| --- | --- | --- |
| A | `Name` | Product name. |
| B | `SKU` | Product SKU. |
| C | `QR` | Barcode or slash-separated aliases. |
| D | `System Inventory` | Expected quantity. |
| E | `Counted` | Counted quantity. |
| F | `Adjustment` | Exported result of `Counted - System Inventory`. |

The reader loads A-E; adjustment is computed from the loaded values. Retain this column order when restoring a previous count workbook. The desktop restore matches imported rows to source products by SKU or a shared barcode alias, skips unmatched rows and nonpositive counts, replaces current counted rows, and resets scan history. The CLI uses the count workbook directly.

## Other exports

- **Unscanned report:** Name, SKU, QR, System Inventory, Counted, and Status. The report includes active products with expected inventory greater than one and no matching scanned barcode.
- **History CSV:** product information, source-product identity, scanned code, action type, machine name, signed quantity, and timestamp.
- **Barcode labels:** Code 128 output, using the first barcode alias when present and SKU as a fallback. Select products and export PDF or PNG pages from the label window.

These reports contain local operational data. Store them outside the source tree; generated workbook, CSV, and PDF files are ignored by the repository.
