using Microsoft.EntityFrameworkCore;
using AdessoCase.Core;
using AdessoCase.Core.Repositories;
using AdessoCase.Core.Enums;

namespace AdessoCase.Repository.Repositories
{
    public class TravelRequestRepository : GenericRepository<TravelRequests>, ITravelRequestsRepository
    {
        public TravelRequestRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Travel?> GetTravelById(int travelId)
        {
            return await _context.Travel.FirstOrDefaultAsync(x => x.Id == travelId && x.Status == (int)TravelStatus.Active && x.TravelDate > DateTime.UtcNow);
        }
    }
}
