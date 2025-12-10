using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation Property
        public ICollection<CartItem> CartItems { get; set; }
    }
}
