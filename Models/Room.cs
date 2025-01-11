using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HotelBooking.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string Type { get; set; } = "Standard";

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsAvailable { get; set; } = true;

        // Relație cu recenziile
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        // Media rating-urilor
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public double AverageRating => Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;
    }
}