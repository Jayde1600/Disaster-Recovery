using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisastersRecovery.Models
{
    public class AvailableMoney
    {
        [Key]
        public int Id { get; set; } // Unique identifier for available money

        [Column(TypeName = "decimal(10,2)")]
        [DisplayName("The Amount Left:")]
        public decimal TotalAmount { get; set; } // Total available funds

        [Column(TypeName = "decimal(10,2)")]
        [DisplayName("The Amount Spent:")]
        public decimal AmountUsed { get; set; } // Amount used for purchases

        // Other properties or relationships as needed
    }
}
