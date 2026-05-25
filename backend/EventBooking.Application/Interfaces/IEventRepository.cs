using EventBooking.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventBooking.Application.Interfaces;

public interface IEventRepository : IGenericRepository<Event>
{
    Task<IEnumerable<Event>> GetEventsByLocationAsync(int locationId);
}