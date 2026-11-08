using ClosedXML.Excel;
using MNInventoryApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace MNInventoryApp.Services
{
    public class ExcelService
    {
        // Reads a source workbook using column headers rather than a store-specific layout.
        public List<Product> LoadProductsFromExcel(string filePath)
        {
            List<Product> products = new List<Product>();

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path is required.");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Excel file not found.", filePath);

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var (headerRow, columns) = FindSourceHeaders(worksheet);

                foreach (var row in worksheet.RowsUsed())
                {
                    if (row.RowNumber() <= headerRow)
                        continue;

                    string sku = row.Cell(columns["sku"]).GetValue<string>().Trim();
                    string name = row.Cell(columns["name"]).GetValue<string>().Trim();
                    string barcode = row.Cell(columns["barcode"]).GetValue<string>().Trim();
                    string inventoryText = row.Cell(columns["systeminventory"]).GetValue<string>().Trim();

                    if (string.IsNullOrWhiteSpace(sku) ||
                        string.IsNullOrWhiteSpace(name) ||
                        string.IsNullOrWhiteSpace(barcode))
                    {
                        continue;
                    }

                    int systemInventory = 0;
                    if (!string.IsNullOrEmpty(inventoryText) &&
                        !int.TryParse(inventoryText, NumberStyles.Integer, CultureInfo.InvariantCulture, out systemInventory))
                    {
                        throw new FormatException($"System Inventory must be a whole number in row {row.RowNumber()}.");
                    }

                    Product product = new Product
                    {
                        SKU = sku,
                        Name = name,
                        QR = barcode,
                        SystemInventory = systemInventory,
                        Counted = 0,
                        IsActive = true
                    };

                    products.Add(product);
                }
            }

            return products;
        }

        private static (int HeaderRow, Dictionary<string, int> Columns) FindSourceHeaders(IXLWorksheet worksheet)
        {
            foreach (var row in worksheet.RowsUsed().Take(10))
            {
                var columns = new Dictionary<string, int>();
                foreach (var cell in row.CellsUsed())
                {
                    string header = new string(cell.GetValue<string>()
                        .Where(char.IsLetterOrDigit).ToArray()).ToLowerInvariant();
                    if (header == "qr")
                        header = "barcode";
                    if (header is "sku" or "name" or "barcode" or "systeminventory")
                        columns.TryAdd(header, cell.Address.ColumnNumber);
                }

                if (columns.Count == 4)
                    return (row.RowNumber(), columns);
            }

            throw new FormatException(
                "The first worksheet must contain headers SKU, Name, Barcode (or QR), and System Inventory within its first 10 used rows.");
        }

        // Legacy / migration use only
        public List<Product> LoadScannedProductsFromExcel(string filePath)
        {
            List<Product> products = new List<Product>();

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path is required.");

            if (!File.Exists(filePath))
                return products;

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);

                foreach (var row in worksheet.RowsUsed())
                {
                    if (row.RowNumber() == 1)
                        continue;

                    string name = row.Cell(1).GetValue<string>().Trim();
                    string sku = row.Cell(2).GetValue<string>().Trim();
                    string qr = row.Cell(3).GetValue<string>().Trim();
                    string systemInventoryText = row.Cell(4).GetValue<string>().Trim();
                    string countedText = row.Cell(5).GetValue<string>().Trim();

                    if (string.IsNullOrWhiteSpace(qr))
                        continue;

                    int systemInventory = 0;
                    int counted = 0;

                    int.TryParse(systemInventoryText, out systemInventory);
                    int.TryParse(countedText, out counted);

                    Product product = new Product
                    {
                        Name = name,
                        SKU = sku,
                        QR = qr,
                        SystemInventory = systemInventory,
                        Counted = counted,
                        IsActive = true
                    };

                    products.Add(product);
                }
            }

            return products;
        }

        // Export only
        public void SaveProductsToExcel(string filePath, List<Product> products)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path is required.");

            if (products == null)
                throw new ArgumentNullException(nameof(products));

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Scanned Products");

                worksheet.Cell(1, 1).Value = "Name";
                worksheet.Cell(1, 2).Value = "SKU";
                worksheet.Cell(1, 3).Value = "QR";
                worksheet.Cell(1, 4).Value = "System Inventory";
                worksheet.Cell(1, 5).Value = "Counted";
                worksheet.Cell(1, 6).Value = "Adjustment";

                int row = 2;

                foreach (Product product in products)
                {
                    worksheet.Cell(row, 1).Value = product.Name;
                    worksheet.Cell(row, 2).Value = product.SKU;
                    worksheet.Cell(row, 3).Value = product.QR;
                    worksheet.Cell(row, 4).Value = product.SystemInventory;
                    worksheet.Cell(row, 5).Value = product.Counted;
                    worksheet.Cell(row, 6).Value = product.Adjustment;
                    row++;
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }
        }
    }
}
