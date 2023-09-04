using AdessoCase.Core.DTOs;
using AdessoCase.Core.Enums;

namespace AdessoCase.Core.Services
{
    public interface ITravelRequestsService : IService<TravelRequests>
    {
        Task CreateNewTravelRequest(TravelRequests travelRequest, CancellationToken cancellationToken);
    }
}
