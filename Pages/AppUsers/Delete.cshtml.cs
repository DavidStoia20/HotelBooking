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
    public class DeleteModel : PageModel
    {
        private readonly HotelBooking.Data.HotelBookingContext _context;

        public DeleteModel(HotelBooking.Data.HotelBookingContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appuser = await _context.AppUser.FindAsync(id);
            if (appuser != null)
            {
                AppUser = appuser;
                _context.AppUser.Remove(AppUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
