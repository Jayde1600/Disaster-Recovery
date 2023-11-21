using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisastersRecovery.Models
{
    public class AllocateGoods
    {
        [Key]
        public int Id { get; set; } // Unique identifier for the allocation

        [Required(ErrorMessage = "Please enter the quantity of goods allocated")]
        public int Quantity { get; set; } // Quantity of goods allocated

        [Required(ErrorMessage = "Please select a disaster")]
        [ForeignKey("DisasterCheck")]
        [DisplayName("Select A Disaster")]
        public int DisasterId { get; set; } // Reference to the disaster being allocated to
        public DisasterCheck? Disaster { get; set; }

        [Required(ErrorMessage = "Please select a type")] // Ensures CategoryId is required
        [ForeignKey("Categories")]
        [DisplayName("Select Type Of Good")]
        public int CategoryId { get; set; }
        public Categories? Category { get; set; }

        /*[Required(ErrorMessage = "Please enter the type/category of goods")]
        public string? Type { get; set; } // Type or category of goods allocated
*/
        [DisplayName("Allocation Date")]
        public DateTime AllocationDate { get; set; } // Date of the allocation

        // Other properties or relationships as needed
    }
}

