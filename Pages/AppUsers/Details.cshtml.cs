using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Data;
using HotelBooking.Models;

namespace HotelBooking.Pages.AppUsers
{
    public class DetailsModel : PageModel
    {
        private readonly HotelBooking.Data.HotelBookingContext _context;

        public DetailsModel(HotelBooking.Data.HotelBookingContext context)
        {
            _context = context;
        }

        public AppUser AppUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appuser = await _context.AppUser.FirstOrDefaultAsync(m => m.Id == id);
            if (appuser == null)
            {
                return NotFound();
            }
            else
            {
                AppUser = appuser;
            }
            return Page();
        }
    }
}
