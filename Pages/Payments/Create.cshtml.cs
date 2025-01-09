using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelBooking.Data;
using HotelBooking.Models;

namespace HotelBooking.Pages.Payments
{
    public class CreateModel : PageModel
    {
        private readonly HotelBooking.Data.HotelBookingContext _context;

        public CreateModel(HotelBooking.Data.HotelBookingContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BookingId"] = new SelectList(_context.Booking, "Id", "Status");
            return Page();
        }

        [BindProperty]
        public Payment Payment { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Payment.Add(Payment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
