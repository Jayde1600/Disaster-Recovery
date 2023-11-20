using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisastersRecovery.Models
{
    public class PurchaseGoods
    {
        [Key]
        public int Id { get; set; } // Unique identifier for the transaction

        [Required(ErrorMessage = "Please enter the name of the item")]
        public string? ItemName { get; set; }

        [Required(ErrorMessage = "Please enter the quantity of the item")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please enter the amount of funds used for purchase")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal AmountUsed { get; set; } // Amount of funds used for the purchase

        public DateTime PurchaseDate { get; set; } // Date of the purchase

        // Other properties or relationships as needed
    }
}
