using EventBooking.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Location> Locations { get; set; }
    public DbSet<Event> Events { get; set; }
}