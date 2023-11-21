using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisastersRecovery.Models
{
    public class AvailableGoods
    {
        [Key]
        public int Id { get; set; } // Unique identifier for available goods

        [Required(ErrorMessage = "Please select a category")] // Ensures CategoryId is required
        [ForeignKey("Categories")]
        [DisplayName("Categories")]
        public int CategoryId { get; set; }
        public Categories? Category { get; set; }

        public int AvailableQuantity { get; set; } // Total available quantity

        public int QuantityUsed { get; set; } // Quantity used from available goods

        // Other properties or relationships as needed
    }
}
