using AutoMapper;
using AdessoCase.Core;
using AdessoCase.Core.DTOs;

namespace AdessoCase.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Travel, TravelDto>().ReverseMap();
            CreateMap<TravelRequests, TravelRequestDto>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
