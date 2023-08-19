using AdessoCase.Core.DTOs;
using AdessoCase.Core.Enums;

namespace AdessoCase.Core.Services
{
    public interface ITravelService : IService<Travel>
    {
        Task<List<FilteredTravelListDto>> FilterTravelAsync(TravelFilterDto filterDto);
        Task ActiveOrPassiveTravelAsync(ChangeTravelStatusDto changeTravelStatusDto);
    }
}
