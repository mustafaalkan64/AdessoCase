using Microsoft.EntityFrameworkCore;
using AdessoCase.Core;
using AdessoCase.Core.Repositories;
using AdessoCase.Core.Enums;

namespace AdessoCase.Repository.Repositories
{
    public class TravelRepository : GenericRepository<Travel>, ITravelRepository
    {
        public TravelRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Travel>> GetTravelsByDepartureAndArrivalAsync(string departure, string arrival)
        {
            IQueryable<Travel> travels = _context.Travel.AsNoTracking().Include(x => x.Departure).Include(x => x.Arrival);
            travels = travels.Where(x => x.Departure.Name.ToLower().Contains(departure.ToLower()));
            travels = travels.Where(x => x.Arrival.Name.ToLower().Contains(arrival.ToLower()));
            travels = travels.Where(x => x.Status == (int)TravelStatus.Active && x.TravelDate > DateTime.UtcNow);
            return await travels.ToListAsync();
        }
    }
}
