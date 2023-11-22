using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisastersRecovery.Models
{
    public class AllocateFunds
    {
        [Key]
        public int Id { get; set; } // Unique identifier for the allocation

        [Required(ErrorMessage = "Please enter the allocated amount")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.00")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; } // Amount of money allocated

        [Required(ErrorMessage = "Please select a disaster")]
        [ForeignKey("DisasterCheck")]
        [DisplayName("Select A Disaster")]
        public int DisasterId { get; set; } // Reference to the disaster being allocated to
        public DisasterCheck? Disaster { get; set; }

        [DisplayName("Allocation Date")]
        public DateTime AllocationDate { get; set; } // Date of the allocation

        // Other properties or relationships as needed
    }
} 
