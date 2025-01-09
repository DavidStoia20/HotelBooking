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
        private readonly HotelBooking.Data.HotelBookingContext _context;

        public CreateModel(HotelBooking.Data.HotelBookingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; }

        public Room Room { get; private set; } // Modificat la private set pentru control mai bun

        [BindProperty(SupportsGet = true)]
        public int RoomId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (RoomId <= 0)
            {
                return NotFound("ID-ul camerei nu a fost transmis corect.");
            }

            // Preluăm camera pe baza RoomId
            Room = await _context.Room.FirstOrDefaultAsync(r => r.Id == RoomId);

            if (Room == null)
            {
                return NotFound("Camera selectată nu există.");
            }

            // Populează Booking cu RoomId pentru afișare în pagină
            Booking = new Booking
            {
                RoomId = RoomId
            };

            Console.WriteLine($"RoomId: {RoomId}");
            Console.WriteLine($"Room: {Room?.RoomNumber ?? "Null"}");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Asigură-te că Room este preluat din baza de date pentru afișare în cazul unei erori
                Room = await _context.Room.FirstOrDefaultAsync(r => r.Id == Booking.RoomId);
                return Page();
            }

            // Validăm dacă RoomId și Room sunt valide
            Room = await _context.Room.FirstOrDefaultAsync(r => r.Id == Booking.RoomId);

            if (Room == null)
            {
                ModelState.AddModelError(string.Empty, "Camera selectată nu mai este disponibilă.");
                return Page();
            }

            // Validare: Check-in și Check-out
            if (Booking.CheckInDate < DateTime.Now.Date)
            {
                ModelState.AddModelError("Booking.CheckInDate", "Data de Check-In trebuie să fie cel puțin astăzi.");
                return Page();
            }

            if (Booking.CheckOutDate <= Booking.CheckInDate)
            {
                ModelState.AddModelError("Booking.CheckOutDate", "Data de Check-Out trebuie să fie după Check-In.");
                return Page();
            }

            // Calculează prețul total pe baza duratei și prețului camerei
            Booking.TotalPrice = (Booking.CheckOutDate - Booking.CheckInDate).Days * Room.Price;

            // Adaugă rezervarea în baza de date
            _context.Booking.Add(Booking);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Rezervare adăugată: RoomId={Booking.RoomId}, TotalPrice={Booking.TotalPrice}");

            return RedirectToPage("./Index");
        }
    }
}