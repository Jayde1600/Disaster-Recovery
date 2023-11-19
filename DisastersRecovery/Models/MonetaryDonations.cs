using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisastersRecovery.Models
{
    public class MonetaryDonation
    {
        [Key]
        public int Id { get; set; } // Unique identifier for the donation

        [DisplayName("Donation Date")]
        public DateTime DonationDate { get; set; } // Date of the donation

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; } // Amount of the donation

        [DisplayName("Anonymous")]
        public bool IsAnonymous { get; set; } // Indicates if the donor wants to remain anonymous

        private string donorName; // Private field to store the donor's name

        [DisplayName("Donor Name")]
        public string DonorName // Donor's name property
        {
            get => IsAnonymous ? "Anonymous" : donorName; // Return "Anonymous" if IsAnonymous is true, otherwise return donorName
            set => donorName = value; // Set the donor's name
        }
        // Other relevant properties can be added as needed
    }
}
