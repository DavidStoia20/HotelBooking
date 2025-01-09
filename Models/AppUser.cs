using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = "User";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
