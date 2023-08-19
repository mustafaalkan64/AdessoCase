using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AdessoCase.API.Filters;
using AdessoCase.Core;
using AdessoCase.Core.DTOs;
using AdessoCase.Core.Services;
using System.ComponentModel.DataAnnotations;

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
            var travel = await _travelService.AddAsync(_mapper.Map<Travel>(travelDto));
            return CreateActionResult(CustomResponseDto<TravelDto>.Success(201, travelDto));
        }

        [HttpPut("change-travel-status")]
        public async Task<IActionResult> Update([FromBody] ChangeTravelStatusDto changeTravelStatusDto)
        {
            var result = await _travelService.ActiveOrPassiveTravelAsync(changeTravelStatusDto.TravelId, changeTravelStatusDto.TravelStatus);

            return CreateActionResult(result);
        }

        [HttpPost("create-travel-requests")]
        public async Task<IActionResult> CreateTravelRequest(TravelRequestDto travelRequestDto)
        {
            var travelRequest = _mapper.Map<TravelRequests>(travelRequestDto);
            travelRequest.UserId = 2;
            var result = await _travelRequestsService.CreateNewTravelRequest(travelRequest);

            return CreateActionResult(result);
        }

        [HttpPost("search-travels")]
        public async Task<IActionResult> SearchTravels([FromBody] TravelFilterDto travelFilterDto)
        {
            var result = await _travelService.FilterTravelAsync(travelFilterDto);
            return CreateActionResult(CustomResponseDto<List<Travel>>.Success(201, result));
        }

    }
}
