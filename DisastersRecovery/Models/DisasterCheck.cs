using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisastersRecovery.Models
{
    public class DisasterCheck
    {
        [Key]
        public int Id { get; set; } // Unique identifier for the disaster

        [Required(ErrorMessage = "Please provide a start date")]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; } // Start date of the disaster

        [Required(ErrorMessage = "Please provide an end date")]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; } // End date of the disaster

        [Required(ErrorMessage = "Location is required")]
        public string? Location { get; set; } // Location of the disaster

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; } // Description of the disaster

        [Required(ErrorMessage = "Aid Type is required")]
        [DisplayName("Aid Type")]
        public string? AidType { get; set; } // Type of aid needed

    }
}
