using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DisastersRecovery.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        public string? CategoryName { get; set; }
    }
}
