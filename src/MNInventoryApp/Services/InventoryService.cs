using MNInventoryApp.Models;
using System;
using System.Collections.Generic;

namespace MNInventoryApp.Services
{
    public class InventoryService
    {
        public Product FindProductByQR(List<Product> products, string qr)
        {
            if (products == null || string.IsNullOrWhiteSpace(qr))
                return null;

            string input = qr.Trim();

            foreach (Product product in products)
            {
                if (product == null || string.IsNullOrWhiteSpace(product.QR))
                    continue;

                string[] barcodes = product.QR.Split('/');

                foreach (string code in barcodes)
                {
                    if (code.Trim() == input)
                    {
                        return product;
                    }
                }
            }

            return null;
        }

        public Product FindProductByQR(Dictionary<string, Product> dictionary, string qr)
        {
            if (dictionary == null || string.IsNullOrWhiteSpace(qr))
                return null;

            string input = qr.Trim();

            if (dictionary.ContainsKey(input))
            {
                return dictionary[input];
            }

            return null;
        }

        public void UpdateCountedQuantity(Product product, int addedQuantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (addedQuantity <= 0)
                throw new ArgumentException("Added quantity must be greater than 0.");

            product.Counted += addedQuantity;
        }

        public InventorySummary GetSummary(List<Product> products)
        {
            InventorySummary summary = new InventorySummary();

            if (products == null)
                return summary;

            summary.TotalProducts = products.Count;

            foreach (Product product in products)
            {
                if (product.Adjustment < 0)
                {
                    summary.TotalMissing += Math.Abs(product.Adjustment);
                }
                else if (product.Adjustment > 0)
                {
                    summary.TotalExtra += product.Adjustment;
                }
            }

            return summary;
        }

        public Dictionary<string, Product> BuildBarcodeDictionary(List<Product> products)
        {
            Dictionary<string, Product> dictionary = new Dictionary<string, Product>();
            HashSet<string> invalidBarcodes = new HashSet<string>();

            if (products == null)
                return dictionary;

            foreach (Product product in products)
            {
                if (product == null || string.IsNullOrWhiteSpace(product.QR))
                    continue;

                string[] barcodes = product.QR.Split('/');

                foreach (string code in barcodes)
                {
                    string cleanCode = code.Trim();

                    if (string.IsNullOrWhiteSpace(cleanCode))
                        continue;

                    if (invalidBarcodes.Contains(cleanCode))
                        continue;

                    if (!dictionary.ContainsKey(cleanCode))
                    {
                        dictionary.Add(cleanCode, product);
                    }
                    else
                    {
                        Product existingProduct = dictionary[cleanCode];

                        bool sameProduct =
                            string.Equals(existingProduct.SKU, product.SKU, StringComparison.OrdinalIgnoreCase) &&
                            string.Equals(existingProduct.Name, product.Name, StringComparison.OrdinalIgnoreCase);

                        if (sameProduct)
                        {
                            // identical duplicate barcode -> keep once
                        }
                        else
                        {
                            // conflicting duplicate barcode -> reject token
                            dictionary.Remove(cleanCode);
                            invalidBarcodes.Add(cleanCode);
                        }
                    }
                }
            }

            return dictionary;
        }

        public List<UnscannedProductReportItem> GetUnscannedProductsReport(
            List<Product> sourceProducts,
            List<Product> scannedProducts)
        {
            List<UnscannedProductReportItem> result = new List<UnscannedProductReportItem>();

            if (sourceProducts == null)
                return result;

            Dictionary<string, Product> scannedDictionary = BuildBarcodeDictionary(scannedProducts);

            foreach (Product sourceProduct in sourceProducts)
            {
                if (sourceProduct == null)
                    continue;

                if (!sourceProduct.IsActive)
                    continue;

                if (string.IsNullOrWhiteSpace(sourceProduct.QR))
                    continue;

                // Report only meaningful stock gaps for store inventory items (> 1 in system).
                if (sourceProduct.SystemInventory <= 1)
                    continue;

                bool foundInScanned = false;
                string[] barcodes = sourceProduct.QR.Split('/');

                foreach (string code in barcodes)
                {
                    string cleanCode = code.Trim();

                    if (string.IsNullOrWhiteSpace(cleanCode))
                        continue;

                    if (scannedDictionary.ContainsKey(cleanCode))
                    {
                        foundInScanned = true;
                        break;
                    }
                }

                if (!foundInScanned)
                {
                    result.Add(new UnscannedProductReportItem
                    {
                        Id = sourceProduct.Id,
                        Name = sourceProduct.Name,
                        SKU = sourceProduct.SKU,
                        QR = sourceProduct.QR,
                        SystemInventory = sourceProduct.SystemInventory,
                        Counted = 0,
                        Status = "Not Scanned",
                        IsActive = sourceProduct.IsActive
                    });
                }
            }

            return result;
        }
    }
}
