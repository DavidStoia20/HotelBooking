using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Data;
using HotelBooking.Models;

namespace HotelBooking.Pages.Reviews
{
    public class IndexModel : PageModel
    {
        private readonly HotelBooking.Data.HotelBookingContext _context;

        public IndexModel(HotelBooking.Data.HotelBookingContext context)
        {
            _context = context;
        }

        public IList<Review> Review { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Review = await _context.Review
                .Include(r => r.Room)
                .Include(r => r.User).ToListAsync();
        }
    }
}
