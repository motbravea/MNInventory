using MNInventoryApp.Models;
using MNInventoryApp.Services;

namespace MNInventoryApp
{
    internal class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 1 && (args[0] == "--help" || args[0] == "-h"))
            {
                PrintUsage();
                return 0;
            }

            if (args.Length != 2)
            {
                PrintUsage();
                return 2;
            }

            var excelService = new ExcelService();
            var inventoryService = new InventoryService();
            string sourceFilePath = Path.GetFullPath(args[0]);
            string workingFilePath = Path.GetFullPath(args[1]);

            if (string.Equals(sourceFilePath, workingFilePath, StringComparison.OrdinalIgnoreCase))
            {
                Console.Error.WriteLine("Use a separate counts workbook so the source inventory is preserved.");
                return 2;
            }

            List<Product> sourceProducts;
            List<Product> scannedProducts;
            Dictionary<string, Product> sourceDictionary;
            Dictionary<string, Product> scannedDictionary;

            Console.WriteLine("===== MNInventory Scanner =====");

            try
            {
                sourceProducts = excelService.LoadProductsFromExcel(sourceFilePath);
                scannedProducts = excelService.LoadScannedProductsFromExcel(workingFilePath);
                sourceDictionary = inventoryService.BuildBarcodeDictionary(sourceProducts);
                scannedDictionary = inventoryService.BuildBarcodeDictionary(scannedProducts);
                Console.WriteLine($"Source products: {sourceProducts.Count}; counted products: {scannedProducts.Count}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Unable to load inventory workbooks: " + ex.Message);
                return 1;
            }

            while (true)
            {
                Console.WriteLine();
                Console.Write("Scan a barcode (or type 'exit'): ");
                string? input = Console.ReadLine();
                if (input == null || string.Equals(input.Trim(), "exit", StringComparison.OrdinalIgnoreCase))
                    break;

                string barcode = input.Trim();
                if (string.IsNullOrWhiteSpace(barcode))
                    continue;

                Product? scannedProduct = inventoryService.FindProductByQR(scannedDictionary, barcode);
                if (scannedProduct == null)
                {
                    Product? sourceProduct = inventoryService.FindProductByQR(sourceDictionary, barcode);
                    if (sourceProduct == null)
                    {
                        Console.WriteLine("Barcode not found in source inventory.");
                        continue;
                    }

                    scannedProduct = new Product
                    {
                        Name = sourceProduct.Name,
                        SKU = sourceProduct.SKU,
                        QR = sourceProduct.QR,
                        SystemInventory = sourceProduct.SystemInventory,
                        Counted = 0
                    };
                    scannedProducts.Add(scannedProduct);
                    scannedDictionary = inventoryService.BuildBarcodeDictionary(scannedProducts);
                }

                Console.WriteLine($"{scannedProduct.Name} | SKU: {scannedProduct.SKU} | Expected: {scannedProduct.SystemInventory} | Counted: {scannedProduct.Counted}");
                Console.Write("Quantity to add: ");
                string? quantityInput = Console.ReadLine();
                if (!int.TryParse(quantityInput, out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("Enter a positive whole number.");
                    continue;
                }

                inventoryService.UpdateCountedQuantity(scannedProduct, quantity);
                try
                {
                    excelService.SaveProductsToExcel(workingFilePath, scannedProducts);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Unable to save the counts workbook: " + ex.Message);
                    return 1;
                }

                InventorySummary summary = inventoryService.GetSummary(scannedProducts);
                Console.WriteLine($"Counted: {scannedProduct.Counted} | Adjustment: {scannedProduct.Adjustment}");
                Console.WriteLine($"Counted products: {summary.TotalProducts} | Missing: {summary.TotalMissing} | Extra: {summary.TotalExtra}");
            }

            Console.WriteLine("Session ended.");
            return 0;
        }

        private static void PrintUsage()
        {
            Console.WriteLine("MNInventory workbook scanner");
            Console.WriteLine("Usage: dotnet run --project src/MNInventoryApp -- <source.xlsx> <counts.xlsx>");
            Console.WriteLine("Source headers: SKU, Name, Barcode, System Inventory (first worksheet).");
            Console.WriteLine("The counts workbook is created when you save your first scan.");
            Console.WriteLine("This console workflow uses local workbooks and does not require MySQL.");
        }
    }
}
