using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AdessoCase.API.Filters;
using AdessoCase.Core;
using AdessoCase.Core.DTOs;
using AdessoCase.Core.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Caching.Memory;

namespace AdessoCase.API.Controllers
{
    public class TravelsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly ITravelService _travelService;
        private readonly ITravelRequestsService _travelRequestsService;

        public TravelsController(IMapper mapper, ITravelService travelService, ITravelRequestsService travelRequestsService)
        {

            _mapper = mapper;
            _travelService = travelService;
            _travelRequestsService = travelRequestsService;
        }

        [HttpPost("create-travel")]
        public async Task<IActionResult> CreateNewTravel([FromBody, Required] TravelDto travelDto)
        {
            var userId = 1;
            travelDto.UserId = userId;
            var travel = _mapper.Map<Travel>(travelDto);
            await _travelService.AddTravelAsync(travel);
            return CreateActionResult(CustomResponseDto<TravelDto>.Success(201, travelDto));
        }

        [HttpPut("change-travel-status")]
        public async Task<IActionResult> Update([FromBody, Required] ChangeTravelStatusDto changeTravelStatusDto)
        {
            var userId = 1; // Current User Id;
            changeTravelStatusDto.UserId = userId;
            await _travelService.ActiveOrPassiveTravelAsync(changeTravelStatusDto);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost("create-travel-requests")]
        public async Task<IActionResult> CreateTravelRequest(TravelRequestDto travelRequestDto)
        {
            var travelRequest = _mapper.Map<TravelRequests>(travelRequestDto);
            travelRequest.UserId = 2;
            await _travelRequestsService.CreateNewTravelRequest(travelRequest);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(201));
        }

        [HttpPost("search-travels")]
        public async Task<IActionResult> SearchTravels([FromBody] TravelFilterDto travelFilterDto)
        {
            var result = await _travelService.FilterTravelAsync(travelFilterDto);
           
            return CreateActionResult(CustomResponseDto<List<TravelListDto>>.Success(200, result));
        }

        [HttpPost("set-travel-cache")]
        public async Task<IActionResult> SetTravelCache()
        {
            await _travelService.SetTravelCache();

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
