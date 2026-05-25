using EventBooking.Application.Interfaces;
using EventBooking.Domain;
using EventBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventBooking.Infrastructure.Repositories;

public class EventRepository : GenericRepository<Event>, IEventRepository
{
    public EventRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Event>> GetEventsByLocationAsync(int locationId)
    {
        return await _context.Events
            .Include(e => e.Location)
            .Where(e => e.LocationId == locationId)
            .ToListAsync();
    }
}