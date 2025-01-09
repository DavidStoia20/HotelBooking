using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(500)]
        public string Comment { get; set; } = string.Empty;

        public AppUser User { get; set; }
        public Room Room { get; set; }
    }
}
