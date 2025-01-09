using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Data;
using HotelBooking.Models;

namespace HotelBooking.Pages.Payments
{
    public class IndexModel : PageModel
    {
        private readonly HotelBooking.Data.HotelBookingContext _context;

        public IndexModel(HotelBooking.Data.HotelBookingContext context)
        {
            _context = context;
        }

        public IList<Payment> Payment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Payment = await _context.Payment
                .Include(p => p.Booking).ToListAsync();
        }
    }
}
