using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using MNInventoryApp.Models;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace MNInventoryUI
{
    /// <summary>
    /// Code128 barcode rendering and compact multi-label PDF/PNG export for retail shelf labels.
    /// </summary>
    public static class BarcodeLabelExportService
    {
        public enum PageKind
        {
            A4,
            Letter
        }

        public sealed class LayoutOptions
        {
            public PageKind PageKind { get; set; } = PageKind.A4;
            public double MarginPoints { get; set; } = 36;
            public double CellGapPoints { get; set; } = 8;
            public int Columns { get; set; } = 3;
            /// <summary>Maximum labels per page (grid capacity); layout may use fewer rows if label height requires it.</summary>
            public int MaxRows { get; set; } = 6;
        }

        /// <summary>
        /// Uses the product QR field (first segment if slash-separated); falls back to SKU.
        /// </summary>
        public static string ResolveBarcodeContent(Product product)
        {
            if (product == null)
                return string.Empty;

            string? qr = product.QR?.Trim();
            if (!string.IsNullOrEmpty(qr))
            {
                string first = qr.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()?.Trim() ?? string.Empty;
                if (!string.IsNullOrEmpty(first))
                    return SanitizeForCode128(first);
            }

            return SanitizeForCode128(product.SKU?.Trim() ?? string.Empty);
        }

        /// <summary>
        /// Code 128 supports full ASCII; remove control characters that break retail scanners or ZXing.
        /// </summary>
        public static string SanitizeForCode128(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            var sb = new StringBuilder(value.Length);
            foreach (char c in value)
            {
                if (c >= 32 && c <= 126)
                    sb.Append(c);
            }

            return sb.ToString().Trim();
        }

        public static Bitmap RenderCode128Bitmap(string content, int widthPx, int heightPx)
        {
            string payload = string.IsNullOrEmpty(content) ? "0" : content;

            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = Math.Max(24, heightPx),
                    Width = Math.Max(80, widthPx),
                    Margin = 2,
                    PureBarcode = true
                }
            };

            PixelData? pixelData = null;
            try
            {
                pixelData = writer.Write(payload);
            }
            catch
            {
                writer.Options = new EncodingOptions
                {
                    Height = Math.Max(24, heightPx),
                    Width = Math.Max(120, widthPx),
                    Margin = 2,
                    PureBarcode = true
                };
                pixelData = writer.Write("ERR");
            }

            Bitmap? raw = pixelData == null ? null : PixelDataToBitmap(pixelData);
            if (raw == null)
                return new Bitmap(1, 1);

            if (raw.Width == widthPx && raw.Height == heightPx)
                return raw;

            try
            {
                var scaled = new Bitmap(widthPx, heightPx, PixelFormat.Format32bppArgb);
                using (var g = Graphics.FromImage(scaled))
                {
                    g.Clear(Color.White);
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.DrawImage(raw, 0, 0, widthPx, heightPx);
                }

                return scaled;
            }
            finally
            {
                raw.Dispose();
            }
        }

        private static Bitmap PixelDataToBitmap(PixelData pixelData)
        {
            var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb);
            BitmapData bmpData = bitmap.LockBits(
                new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppRgb);

            int length = pixelData.Pixels.Length;
            if (bmpData.Stride * pixelData.Height == length)
            {
                Marshal.Copy(pixelData.Pixels, 0, bmpData.Scan0, length);
            }
            else
            {
                int srcIndex = 0;
                for (int y = 0; y < pixelData.Height; y++)
                {
                    IntPtr dstRow = bmpData.Scan0 + y * bmpData.Stride;
                    int rowBytes = Math.Min(pixelData.Width * 4, bmpData.Stride);
                    Marshal.Copy(pixelData.Pixels, srcIndex, dstRow, rowBytes);
                    srcIndex += pixelData.Width * 4;
                }
            }

            bitmap.UnlockBits(bmpData);
            return bitmap;
        }

        /// <summary>
        /// Renders a single label block: name, SKU, quantity, Code128 image, human-readable number.
        /// </summary>
        public static Bitmap RenderLabelBitmap(Product product, int widthPx, int heightPx)
        {
            var bmp = new Bitmap(widthPx, heightPx, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                string barcodeText = ResolveBarcodeContent(product);
                if (string.IsNullOrEmpty(barcodeText))
                    barcodeText = "—";

                using var fontTitle = new Font("Segoe UI", 8.25f, FontStyle.Bold, GraphicsUnit.Point);
                using var fontBody = new Font("Segoe UI", 7.5f, FontStyle.Regular, GraphicsUnit.Point);
                using var fontHr = new Font("Consolas", 7f, FontStyle.Regular, GraphicsUnit.Point);
                using var brush = new SolidBrush(Color.Black);
                using var format = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.LineLimit };

                float pad = 4f;
                float y = pad;

                string name = product.Name ?? string.Empty;
                var titleRect = new RectangleF(pad, y, widthPx - pad * 2, fontTitle.GetHeight(g) * 2.2f);
                g.DrawString(name, fontTitle, brush, titleRect, format);
                y = titleRect.Bottom + 2f;

                g.DrawString("SKU: " + (product.SKU ?? ""), fontBody, brush, pad, y);
                y += fontBody.GetHeight(g) + 1f;

                g.DrawString("Qty: " + product.Counted, fontBody, brush, pad, y);
                y += fontBody.GetHeight(g) + 3f;

                int barcodeH = Math.Max(32, (int)(heightPx - y - pad - fontHr.GetHeight(g) - 4));
                int barcodeW = widthPx - (int)(pad * 2);

                using (Bitmap? bc = RenderCode128Bitmap(barcodeText, barcodeW, barcodeH))
                {
                    g.DrawImage(bc, pad, y, barcodeW, barcodeH);
                    y += barcodeH + 2f;
                }

                g.DrawString(barcodeText, fontHr, brush, new RectangleF(pad, y, widthPx - pad * 2, fontHr.GetHeight(g) + 2),
                    new StringFormat { Alignment = StringAlignment.Center });
            }

            return bmp;
        }

        public static void ExportToPdf(IReadOnlyList<Product> products, string filePath, LayoutOptions? options = null)
        {
            if (products == null || products.Count == 0)
                throw new ArgumentException("No products to export.", nameof(products));

            options ??= new LayoutOptions();
            var document = new PdfDocument();
            document.Info.Title = "Barcode labels";
            document.Info.Creator = "MNInventory";

            foreach (var pageChunk in ChunkByPageCapacity(products, options))
            {
                var page = document.AddPage();
                ApplyPageSize(page, options.PageKind);
                double pageW = page.Width.Point;
                double pageH = page.Height.Point;

                double margin = options.MarginPoints;
                double gap = options.CellGapPoints;
                int cols = Math.Max(1, options.Columns);
                int maxRows = Math.Max(1, options.MaxRows);

                double innerW = pageW - 2 * margin;
                double innerH = pageH - 2 * margin;

                double labelW = (innerW - (cols - 1) * gap) / cols;
                double labelH = Math.Min(118, (innerH - (maxRows - 1) * gap) / maxRows);

                using (var gfx = XGraphics.FromPdfPage(page))
                {
                    int index = 0;
                    foreach (Product p in pageChunk)
                    {
                        int row = index / cols;
                        int col = index % cols;
                        if (row >= maxRows)
                            break;

                        double x = margin + col * (labelW + gap);
                        double y = margin + row * (labelH + gap);

                        gfx.DrawRectangle(XPens.LightGray, x, y, labelW, labelH);

                        using Bitmap labelBmp = RenderLabelBitmapForPdf(p, labelW, labelH);
                        using (var ms = new MemoryStream())
                        {
                            labelBmp.Save(ms, ImageFormat.Png);
                            ms.Position = 0;
                            using var ximg = XImage.FromStream(() => ms);
                            gfx.DrawImage(ximg, x + 1, y + 1, labelW - 2, labelH - 2);
                        }

                        index++;
                    }
                }
            }

            document.Save(filePath);
        }

        /// <summary>
        /// Exports one PNG file per printed page (same breaks as PDF). Returns the list of written paths.
        /// </summary>
        public static IReadOnlyList<string> ExportToPngPages(
            IReadOnlyList<Product> products,
            string outputDirectory,
            string fileNamePrefix,
            LayoutOptions? options = null)
        {
            if (products == null || products.Count == 0)
                throw new ArgumentException("No products to export.", nameof(products));

            if (string.IsNullOrWhiteSpace(outputDirectory))
                throw new ArgumentException("Output directory is required.", nameof(outputDirectory));

            Directory.CreateDirectory(outputDirectory);
            options ??= new LayoutOptions();

            var written = new List<string>();
            int pageNumber = 1;

            foreach (var pageChunk in ChunkByPageCapacity(products, options))
            {
                (double pageWpt, double pageHpt) = GetPageSizePoints(options.PageKind);
                double margin = options.MarginPoints;
                double gap = options.CellGapPoints;
                int cols = Math.Max(1, options.Columns);
                int maxRows = Math.Max(1, options.MaxRows);

                double innerW = pageWpt - 2 * margin;
                double innerH = pageHpt - 2 * margin;
                double labelW = (innerW - (cols - 1) * gap) / cols;
                double labelH = Math.Min(118, (innerH - (maxRows - 1) * gap) / maxRows);

                int dpi = 200;
                double scale = dpi / 72.0;
                int bmpW = (int)Math.Ceiling(pageWpt * scale);
                int bmpH = (int)Math.Ceiling(pageHpt * scale);

                using var pageBmp = new Bitmap(bmpW, bmpH, PixelFormat.Format32bppArgb);
                pageBmp.SetResolution(dpi, dpi);
                using (var g = Graphics.FromImage(pageBmp))
                {
                    g.Clear(Color.White);
                    g.PageUnit = GraphicsUnit.Pixel;
                    g.ScaleTransform((float)scale, (float)scale);

                    int index = 0;
                    foreach (Product p in pageChunk)
                    {
                        int row = index / cols;
                        int col = index % cols;
                        if (row >= maxRows)
                            break;

                        double x = margin + col * (labelW + gap);
                        double y = margin + row * (labelH + gap);

                        using var pen = new Pen(Color.LightGray);
                        g.DrawRectangle(pen, (float)x, (float)y, (float)labelW, (float)labelH);

                        using Bitmap labelBmp = RenderLabelBitmapForPdf(p, labelW, labelH);
                        g.DrawImage(labelBmp, (float)(x + 1), (float)(y + 1), (float)(labelW - 2), (float)(labelH - 2));

                        index++;
                    }
                }

                string safePrefix = string.IsNullOrWhiteSpace(fileNamePrefix) ? "labels" : fileNamePrefix;
                string fileName = $"{safePrefix}_page{pageNumber:00}.png";
                string fullPath = Path.Combine(outputDirectory, fileName);
                pageBmp.Save(fullPath, ImageFormat.Png);
                written.Add(fullPath);
                pageNumber++;
            }

            return written;
        }

        /// <summary>
        /// Renders a single label bitmap sized for the PDF cell (points converted to pixels at 150 DPI for crisp print).
        /// </summary>
        private static Bitmap RenderLabelBitmapForPdf(Product product, double labelWPt, double labelHPt)
        {
            const double targetDpi = 150;
            int wPx = Math.Max(80, (int)Math.Ceiling(labelWPt * targetDpi / 72.0));
            int hPx = Math.Max(60, (int)Math.Ceiling(labelHPt * targetDpi / 72.0));
            return RenderLabelBitmap(product, wPx, hPx);
        }

        private static void ApplyPageSize(PdfPage page, PageKind kind)
        {
            (double w, double h) = GetPageSizePoints(kind);
            page.Width = XUnit.FromPoint(w);
            page.Height = XUnit.FromPoint(h);
        }

        private static (double W, double H) GetPageSizePoints(PageKind kind)
        {
            return kind switch
            {
                PageKind.Letter => (612, 792),
                _ => (595, 842)
            };
        }

        private static IEnumerable<List<Product>> ChunkByPageCapacity(IReadOnlyList<Product> products, LayoutOptions options)
        {
            int cols = Math.Max(1, options.Columns);
            int maxRows = Math.Max(1, options.MaxRows);
            int perPage = cols * maxRows;

            var chunk = new List<Product>(perPage);
            foreach (Product p in products)
            {
                chunk.Add(p);
                if (chunk.Count >= perPage)
                {
                    yield return chunk;
                    chunk = new List<Product>(perPage);
                }
            }

            if (chunk.Count > 0)
                yield return chunk;
        }
    }
}
