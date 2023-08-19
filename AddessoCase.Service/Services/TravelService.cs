using AutoMapper;
using AdessoCase.Core;
using AdessoCase.Core.DTOs;
using AdessoCase.Core.Repositories;
using AdessoCase.Core.Services;
using AdessoCase.Core.UnitOfWorks;
using System.Linq.Expressions;
using AdessoCase.Core.Enums;
using AdessoCase.Repository.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using AdessoCase.Service.Exceptions;

namespace AdessoCase.Service.Services
{
    public class TravelService : Service<Travel>, ITravelService
    {
        private readonly ITravelRepository _travelRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TravelService(IGenericRepository<Travel> repository, IUnitOfWork unitOfWork, IMapper mapper, ITravelRepository travelRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _travelRepository = travelRepository;
            _unitOfWork = unitOfWork;   
        }

        public async Task<CustomResponseDto<NoContentDto>> ActiveOrPassiveTravelAsync(int travelId, TravelStatus status)
        {
            var travel = await _travelRepository.GetByIdAsync(travelId);
            if(travel == null)
                throw new NotFoundExcepiton($"{typeof(Travel).Name}({travelId}) not found");
            
            travel.Status = (int)status;
            _travelRepository.Update(travel);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(200);
        }

        public async Task<List<FilteredTravelListDto>> FilterTravelAsync(TravelFilterDto filterDto)
        {
            var result = await _travelRepository.GetTravelsByDepartureAndArrivalAsync(filterDto.Departure, filterDto.Arrival);
            var dtoList = result.Select(x => new FilteredTravelListDto()
            {
                Arrival = x.Arrival.Name,
                Departure = x.Departure.Name,
                Id = x.Id,
                Description = x.Description,
                SeatCount = x.SeatCount,
                Status = ((TravelStatus)x.Status).ToString(),
                TravelDate = x.TravelDate
            }).ToList();

            return dtoList;
        }
    }
}
