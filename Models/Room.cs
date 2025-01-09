using System.ComponentModel.DataAnnotations;

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
    }
}
