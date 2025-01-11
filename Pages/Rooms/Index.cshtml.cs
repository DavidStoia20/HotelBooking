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

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; } // Nouă proprietate pentru sortare

        public async Task OnGetAsync(string Type, decimal? MaxPrice, string SortBy)
        {
            var roomsQuery = _context.Room.AsQueryable();

            // Aplică filtrul pentru tipul camerei
            if (!string.IsNullOrEmpty(Type))
            {
                roomsQuery = roomsQuery.Where(r => r.Type == Type);
            }

            // Aplică filtrul pentru preț
            if (MaxPrice.HasValue)
            {
                roomsQuery = roomsQuery.Where(r => r.Price <= MaxPrice.Value);
            }

            // Aplică sortarea
            if (SortBy == "PriceAsc")
            {
                roomsQuery = roomsQuery.OrderBy(r => r.Price);
            }
            else if (SortBy == "PriceDesc")
            {
                roomsQuery = roomsQuery.OrderByDescending(r => r.Price);
            }
            else if (SortBy == "Type")
            {
                roomsQuery = roomsQuery.OrderBy(r => r.Type);
            }

            // Obține lista de camere filtrate și sortate
            Rooms = await roomsQuery.ToListAsync();
        }
    }
}