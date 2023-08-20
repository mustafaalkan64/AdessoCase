﻿using AdessoCase.Core.DTOs;
using AdessoCase.Core.Enums;

namespace AdessoCase.Core.Services
{
    public interface ITravelService : IService<Travel>
    {
        Task AddTravelAsync(Travel travel);
        Task<List<TravelListDto>> FilterTravelAsync(TravelFilterDto filterDto);
        Task ActiveOrPassiveTravelAsync(ChangeTravelStatusDto changeTravelStatusDto);
        Task SetTravelCache();
    }
}
