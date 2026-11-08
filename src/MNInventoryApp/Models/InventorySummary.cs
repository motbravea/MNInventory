namespace MNInventoryApp.Models
{
    public class InventorySummary
    {
        public int TotalProducts { get; set; }

        // Positive number of missing units
        public int TotalMissing { get; set; }

        // Positive number of extra units
        public int TotalExtra { get; set; }
    }
}
