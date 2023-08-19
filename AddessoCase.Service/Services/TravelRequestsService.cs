﻿using AutoMapper;
using AdessoCase.Core;
using AdessoCase.Core.DTOs;
using AdessoCase.Core.Repositories;
using AdessoCase.Core.Services;
using AdessoCase.Core.UnitOfWorks;
using AdessoCase.Service.Exceptions;

namespace AdessoCase.Service.Services
{
    public class TravelRequestsService : Service<TravelRequests>, ITravelRequestsService
    {
        private readonly ITravelRequestsRepository _travelRequestsRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TravelRequestsService(IGenericRepository<TravelRequests> repository, IUnitOfWork unitOfWork, IMapper mapper, ITravelRequestsRepository travelRequestsRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _travelRequestsRepository = travelRequestsRepository;
            _unitOfWork = unitOfWork;   
        }

        public async Task<CustomResponseDto<NoContentDto>> CreateNewTravelRequest(TravelRequests travelRequest)
        {
            var travel = await _travelRequestsRepository.GetTravelById(travelRequest.TravelId);
            if (travel == null)
                throw new NotFoundExcepiton($"{typeof(Travel).Name}({travelRequest.TravelId}) not found");

            if (travel.SeatCount < 1)
                return CustomResponseDto<NoContentDto>.Fail(400, "Selected Travel Has No Seat, Please Search Another Travel");

            await _travelRequestsRepository.AddAsync(travelRequest);
            travel.SeatCount = travel.SeatCount - 1;
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(200);
        }
    }
}