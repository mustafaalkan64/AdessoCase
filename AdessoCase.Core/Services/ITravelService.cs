using AdessoCase.Core.DTOs;
using AdessoCase.Core.Enums;

namespace AdessoCase.Core.Services
{
    public interface ITravelService : IService<Travel>
    {
        Task AddTravelAsync(Travel travel, CancellationToken cancellationToken);
        Task<List<TravelListDto>> FilterTravelAsync(TravelFilterDto filterDto, CancellationToken cancellationToken = default);
        Task ActiveOrPassiveTravelAsync(ChangeTravelStatusDto changeTravelStatusDto, CancellationToken cancellationToken = default);
        Task SetTravelCache();
    }
}
