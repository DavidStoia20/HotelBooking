using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Pages.Bookings
{
    public class CreateModel : PageModel
    {
        private readonly HotelBookingContext _context;

        public CreateModel(HotelBookingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; }

        public Room Room { get; set; }

        [BindProperty(SupportsGet = true)]
        public int RoomId { get; set; }

        // Metoda OnGetAsync pentru preluarea camerei din baza de date pe baza roomId
        public async Task<IActionResult> OnGetAsync(int roomId)
        {
            if (roomId <= 0)
            {
                return RedirectToPage("/Rooms/Index"); // Redirecționează la lista de camere dacă roomId lipsește sau este invalid
            }

            // Verifică camera în baza de date
            Room = await _context.Room.FirstOrDefaultAsync(r => r.Id == roomId);

            if (Room == null)
            {
                Console.WriteLine($"Camera cu id-ul {roomId} nu a fost găsită.");
                return NotFound("Camera selectată nu există.");
            }

            return Page(); // Returnează pagina dacă camera este găsită
        }


        // Metoda OnPostAsync pentru procesarea datelor trimise prin POST (rezervare)
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Dacă modelul nu este valid, rămâne pe aceeași pagină
            }

            // Verificăm dacă camera selectată este disponibilă
            Room = await _context.Room.FirstOrDefaultAsync(r => r.Id == Booking.RoomId);
            if (Room == null || !Room.IsAvailable)
            {
                ModelState.AddModelError(string.Empty, "Camera selectată nu mai este disponibilă.");
                return Page(); // Dacă camera nu este disponibilă, rămâne pe aceeași pagină
            }

            // Verificăm disponibilitatea pe perioada aleasă
            var existingBookings = await _context.Booking
                .Where(b => b.RoomId == Booking.RoomId &&
                           ((b.CheckInDate >= Booking.CheckInDate && b.CheckInDate < Booking.CheckOutDate) ||
                            (b.CheckOutDate > Booking.CheckInDate && b.CheckOutDate <= Booking.CheckOutDate)))
                .ToListAsync();

            if (existingBookings.Any())
            {
                ModelState.AddModelError(string.Empty, "Camera selectată nu mai este disponibilă pentru perioada aleasă.");
                return Page();
            }

            // Calculăm prețul total
            Booking.TotalPrice = (Booking.CheckOutDate - Booking.CheckInDate).Days * Room.Price;

            // Adăugăm rezervarea în baza de date
            _context.Booking.Add(Booking);
            await _context.SaveChangesAsync();

            // Redirecționează utilizatorul către o altă pagină (de exemplu, pagina principală a rezervărilor)
            return RedirectToPage("./Index");
        }
    }
}
