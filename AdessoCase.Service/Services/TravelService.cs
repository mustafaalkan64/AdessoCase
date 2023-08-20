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
using Microsoft.Extensions.Caching.Memory;
using AdessoCase.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdessoCase.Service.Services
{
    public class TravelService : Service<Travel>, ITravelService
    {
        private const string CacheTravelKey = "TravelsCache";
        private readonly ITravelRepository _travelRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memCache;

        public TravelService(IGenericRepository<Travel> repository, IMemoryCache memCache, IUnitOfWork unitOfWork, IMapper mapper, ITravelRepository travelRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _travelRepository = travelRepository;
            _unitOfWork = unitOfWork;   
            _memCache = memCache;
        }

        public async Task ActiveOrPassiveTravelAsync(ChangeTravelStatusDto changeTravelStatusDto)
        {
            var travel = await _travelRepository.GetByIdAsync(changeTravelStatusDto.TravelId);
            if(travel == null || travel.UserId != changeTravelStatusDto.UserId)
                throw new NotFoundExcepiton($"{typeof(Travel).Name} With ({changeTravelStatusDto.TravelId}) Id, not found");
            
            travel.Status = (int)changeTravelStatusDto.TravelStatus;
            _travelRepository.Update(travel);

            if (!_memCache.TryGetValue(CacheTravelKey, out _))
            {
                var travelList = _memCache.Get<List<TravelListDto>>(CacheTravelKey);
                var cache = travelList.FirstOrDefault(x => x.Id == travel.Id);
                cache.Status = changeTravelStatusDto.TravelStatus.ToString();
                _memCache.Set(CacheTravelKey, travelList);
            }
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<TravelListDto>> FilterTravelAsync(TravelFilterDto filterDto)
        {
            // TODO: In order to improve performance, i used memory cache. But it desire, you can implement Elastic Search for search quickly
            if(!_memCache.TryGetValue(CacheTravelKey, out _))
            {
                var result = await _travelRepository.GetTravelsByDepartureAndArrivalAsync(filterDto.From, filterDto.To);
                var dtoList = result.Select(x => new TravelListDto(x)).ToList();

                return dtoList;
            }
            else
            {
                var travelList = _memCache.Get<IEnumerable<TravelListDto>>(CacheTravelKey);
                if (!string.IsNullOrEmpty(filterDto.From))
                    //travelList = travelList.Where(x => x.Departure.Contains(filterDto.From, StringComparison.OrdinalIgnoreCase)); // Does not work for İzmir!
                    travelList = travelList.Where(x => x.Departure.ToLower().Contains(filterDto.From.ToLower()));

                if (!string.IsNullOrEmpty(filterDto.To))
                    travelList = travelList.Where(x => x.Arrival.ToLower().Contains(filterDto.To.ToLower()));

                travelList = travelList.Where(x => x.Status == TravelStatus.Active.ToString() && x.TravelDate > DateTime.UtcNow && x.SeatCount > 0);
                return travelList.ToList();
            }

        }

        public async Task AddTravelAsync(Travel travel)
        {
            travel.TravelDate = travel.TravelDate.ToUniversalTime();
            await _travelRepository.AddAsync(travel);
            await _unitOfWork.CommitAsync();


            if (!_memCache.TryGetValue(CacheTravelKey, out _))
            {

                var dtoList = (await _travelRepository.GetAllWithLocaltions()).Select(x => new TravelListDto(x)).ToList();
                _memCache.Set(CacheTravelKey, dtoList);
            }
            else
            {
                // TODO: We have allready travel obj, if we get cities from cache (city data is constant allready), we dont need to get query from sql twice time..
                var travelList = _memCache.Get<List<TravelListDto>>(CacheTravelKey);
                var result = await _travelRepository.GetByIdWithLocaltions(travel.Id);
                travelList.Add(new TravelListDto(result));
                _memCache.Set(CacheTravelKey, travelList);
            }
        }


        public async Task SetTravelCache()
        {
            if (!_memCache.TryGetValue(CacheTravelKey, out _))
            {

                var dtoList = (await _travelRepository.GetAllWithLocaltions()).Select(x => new TravelListDto(x)).ToList();
                _memCache.Set(CacheTravelKey, dtoList);
            }
        }
    }
}
