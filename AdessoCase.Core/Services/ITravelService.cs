using AdessoCase.Core.DTOs;
using AdessoCase.Core.Enums;

namespace AdessoCase.Core.Services
{
    public interface ITravelService : IService<Travel>
    {
        Task<List<Travel>> FilterTravelAsync(TravelFilterDto filterDto);
        Task<CustomResponseDto<NoContentDto>> ActiveOrPassiveTravelAsync(int travelId, TravelStatus status);
    }
}
