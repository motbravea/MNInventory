namespace MNInventoryApp.Models
{
    public class Product
    {
        // Database identity
        public int Id { get; set; }

        // Product info
        public string Name { get; set; }
        public string SKU { get; set; }
        public string QR { get; set; }

        // Inventory values
        public int SystemInventory { get; set; }
        public int Counted { get; set; }

        // Source product state
        public bool IsActive { get; set; } = true;

        // Computed value
        public int Adjustment
        {
            get { return Counted - SystemInventory; }
        }
    }
}
