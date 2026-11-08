namespace MNInventoryApp.Models
{
    public class UnscannedProductReportItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string SKU { get; set; }
        public string QR { get; set; }

        public int SystemInventory { get; set; }
        public int Counted { get; set; }

        public string Status { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
