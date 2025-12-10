using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.Models
{
    public class Wishlist
    {
        [Key]
        public int WishlistId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.Now;

        // Navigation Property
        public Product Product { get; set; }
    }
}
