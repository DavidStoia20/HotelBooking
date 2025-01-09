using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Models;

namespace HotelBooking.Data
{
    public class HotelBookingContext : DbContext
    {
        public HotelBookingContext (DbContextOptions<HotelBookingContext> options)
            : base(options)
        {
        }

        public DbSet<HotelBooking.Models.AppUser> AppUser { get; set; } = default!;
        public DbSet<HotelBooking.Models.Booking> Booking { get; set; } = default!;
        public DbSet<HotelBooking.Models.Payment> Payment { get; set; } = default!;
        public DbSet<HotelBooking.Models.Review> Review { get; set; } = default!;
        public DbSet<HotelBooking.Models.Room> Room { get; set; } = default!;
    }
}
