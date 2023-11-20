using System.ComponentModel.DataAnnotations;

namespace DisastersRecovery.Models
{
    public class AvailableGoods
    {
        [Key]
        public int Id { get; set; } // Unique identifier for available goods

        public int GoodsId { get; set; } // Reference to the specific goods (from GoodsDonations or PurchaseGoods)

        public int AvailableQuantity { get; set; } // Total available quantity

        // Other properties or relationships as needed
    }
}
