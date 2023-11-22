using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DisastersRecovery.Models
{
    public class GoodsDonation
    {
        [Key]
        public int Id { get; set; } // Unique identifier for the donation

        [DisplayName("Donation Date")]
        public DateTime DonationDate { get; set; } // Date of the donation

        [DisplayName("Number Of Items")]
        [Range(1, int.MaxValue, ErrorMessage = "Number Of Items must be a whole number and greater than 0")]
        public int NumberOfItems { get; set; } // Number of donated items

        [Required(ErrorMessage = "Please select a category")] // Ensures CategoryId is required
        [ForeignKey("Categories")]
        [DisplayName("Categories")]
        public int CategoryId { get; set; }
        public Categories? Category { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(23, ErrorMessage = "Description must be a maximum of 23 characters")]
        public string? Description { get; set; } // Description of the donated items

        [DisplayName("Anonymous")]
        public bool IsAnonymous { get; set; } // Indicates if the donor wants to remain anonymous

        private string donorName; // Private field to store the donor's name

        [DisplayName("Donor Name")]
        public string DonorName // Donor's name property
        {
            get => IsAnonymous ? "Anonymous" : donorName; // Return "Anonymous" if IsAnonymous is true, otherwise return donorName
            set => donorName = value; // Set the donor's name
        }
    }
}
