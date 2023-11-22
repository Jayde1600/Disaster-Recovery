using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisastersRecovery.Models
{
    public class ActiveDisasters
    {
        [Key]
        public int DisasterCheckId { get; set; } // Reference to the disaster

        [DisplayName("Disaster")]
        public string Disaster { get; set; } // Description of the disaster

        public string Location { get; set; } // Location of the disaster

        [DisplayName("End Date")]
        public DateTime EndDate { get; set; } // End date of the disaster

        [DisplayName("Donated Amount")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal AllocatedAmount { get; set; } // Amount of money allocated

        [DisplayName("Donated Goods")]
        public int AllocatedQuantity { get; set; } // Quantity of goods allocated

        [DisplayName("Category")]
        public string GoodsCategory { get; set; } // Type or category of goods allocated

        // Other properties or relationships as needed
    }
}
