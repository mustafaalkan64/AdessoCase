using Microsoft.EntityFrameworkCore;
using AdessoCase.Core;
using AdessoCase.Core.Repositories;
using AdessoCase.Core.Enums;
using System.Threading;

namespace AdessoCase.Repository.Repositories
{
    public class TravelRequestRepository : GenericRepository<TravelRequests>, ITravelRequestsRepository
    {
        public TravelRequestRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Travel?> GetTravelById(int travelId, CancellationToken cancellationToken = default)
        {
            return await _context.Travel.FirstOrDefaultAsync(x => x.Id == travelId && x.Status == (int)TravelStatus.Active && x.TravelDate > DateTime.UtcNow, cancellationToken);
        }
    }
}
