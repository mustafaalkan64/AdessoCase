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

        public async Task<List<Travel>> GetTravelsByDepartureAndArrivalAsync(string from, string to)
        {
            IQueryable<Travel> travels = _context.Travel.AsNoTracking().Include(x => x.Departure).Include(x => x.Arrival);
            if(!String.IsNullOrEmpty(from))
                travels = travels.Where(x => x.Departure.Name.ToLower().Contains(from.ToLower()));
            
            if (!String.IsNullOrEmpty(to))
                travels = travels.Where(x => x.Arrival.Name.ToLower().Contains(to.ToLower()));

            travels = travels.Where(x => x.Status == (int)TravelStatus.Active && x.TravelDate > DateTime.UtcNow && x.SeatCount > 0);
            return await travels.ToListAsync();
        }

        public async Task<List<Travel>> GetAllWithLocaltions()
        {
            return await _context.Travel.AsNoTracking().Include(x => x.Departure).Include(x => x.Arrival).ToListAsync();
        }

        public async Task<Travel?> GetByIdWithLocaltions(int travelId)
        {
            return await _context.Travel.Include(x => x.Departure).Include(x => x.Arrival).FirstOrDefaultAsync(x => x.Id == travelId);
        }
    }
}
