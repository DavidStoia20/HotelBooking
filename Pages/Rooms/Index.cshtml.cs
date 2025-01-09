using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Data;
using HotelBooking.Models;

namespace HotelBooking.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly HotelBooking.Data.HotelBookingContext _context;

        public IndexModel(HotelBooking.Data.HotelBookingContext context)
        {
            _context = context;
        }

        public IList<Room> Rooms { get; set; } = new List<Room>();

        [BindProperty(SupportsGet = true)]
        public string Type { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MaxPrice { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Room != null)
            {
                // Construim o interogare de bază pentru camere
                var query = _context.Room.AsQueryable();

                // Filtrare după tipul camerei
                if (!string.IsNullOrEmpty(Type))
                {
                    query = query.Where(r => r.Type == Type);
                }

                // Filtrare după preț maxim
                if (MaxPrice.HasValue)
                {
                    query = query.Where(r => r.Price <= MaxPrice.Value);
                }

                // Obține lista filtrată
                Rooms = await query.ToListAsync();
            }
        }
    }
}
