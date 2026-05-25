using EventBooking.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventBooking.Application.Interfaces;

public interface ILocationRepository : IGenericRepository<Location>
{
    Task<IEnumerable<Location>> GetLocationsWithEventsAsync();
}