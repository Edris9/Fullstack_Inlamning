using EventBooking.Application.Interfaces;
using EventBooking.Domain;
using EventBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventBooking.Infrastructure.Repositories;

public class LocationRepository : GenericRepository<Location>, ILocationRepository
{
    public LocationRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Location>> GetLocationsWithEventsAsync()
    {
        return await _context.Locations
            .Include(l => l.Events)
            .ToListAsync();
    }
}