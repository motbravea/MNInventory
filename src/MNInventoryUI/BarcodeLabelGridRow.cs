using System;
using MNInventoryApp.Models;

namespace MNInventoryUI
{
    /// <summary>
    /// View-model row for barcode label selection grid (binds to scanned <see cref="Product"/>).
    /// </summary>
    public sealed class BarcodeLabelGridRow
    {
        public BarcodeLabelGridRow(Product product)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Name = product.Name ?? string.Empty;
            Sku = product.SKU ?? string.Empty;
            Qty = product.Counted;
            BarcodeRaw = BarcodeLabelExportService.ResolveBarcodeContent(product);
        }

        public bool Selected { get; set; }

        public string Name { get; }

        public string Sku { get; }

        public int Qty { get; }

        public string BarcodeRaw { get; }

        public Product Product { get; }
    }
}
