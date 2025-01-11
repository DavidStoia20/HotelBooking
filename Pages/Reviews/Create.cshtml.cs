using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Pages.Reviews
{
    public class CreateModel : PageModel
    {
        private readonly HotelBookingContext _context;

        public CreateModel(HotelBookingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Review Review { get; set; }

        [BindProperty]
        public string RoomNumber { get; set; }

        public SelectList RoomNumbers { get; set; } // Dropdown pentru camere

        public async Task<IActionResult> OnGetAsync()
        {
            // Populăm dropdown-ul cu numerele camerelor
            RoomNumbers = new SelectList(await _context.Room.Select(r => r.RoomNumber).ToListAsync());
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Re-populăm dropdown-ul în caz de eroare
                RoomNumbers = new SelectList(await _context.Room.Select(r => r.RoomNumber).ToListAsync());
                return Page();
            }

            // Găsim RoomId pe baza RoomNumber selectat
            var room = await _context.Room.FirstOrDefaultAsync(r => r.RoomNumber == RoomNumber);
            if (room == null)
            {
                ModelState.AddModelError(string.Empty, "Camera selectată nu există.");
                RoomNumbers = new SelectList(await _context.Room.Select(r => r.RoomNumber).ToListAsync());
                return Page();
            }

            // Asociem RoomId cu recenzia
            Review.RoomId = room.Id;

            // Salvăm recenzia
            _context.Review.Add(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}