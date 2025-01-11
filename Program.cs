using Microsoft.EntityFrameworkCore;
using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Adăugăm contextul bazelor de date doar o dată
builder.Services.AddDbContext<HotelBookingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelBookingContext") ??
                         throw new InvalidOperationException("Connection string 'HotelBookingContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityContext>();

// Adăugăm serviciile necesare pentru Razor Pages
builder.Services.AddRazorPages();

builder.Services.AddDbContext<IdentityContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("HotelBookingContext") ?? throw new InvalidOperationException("Connectionstring 'HotelBookingContext' not found.")));

var app = builder.Build();

// Configurăm pipeline-ul HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
