using System;

namespace MNInventoryApp.Models
{
    public class ScanHistoryItem
    {
        public int SourceProductId { get; set; }

        public string Name { get; set; }
        public string SKU { get; set; }
        public string QR { get; set; }

        public string ScannedCode { get; set; }
        public int QuantityAdded { get; set; }
        public string ActionType { get; set; }
        public string MachineName { get; set; }

        public DateTime ScanTime { get; set; }
    }
}
