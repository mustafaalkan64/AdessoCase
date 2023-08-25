using FluentValidation;
using AdessoCase.Core.DTOs;

namespace AdessoCase.Service.Validations
{
    public class TravelDtoValidator : AbstractValidator<TravelDto>
    {
        public TravelDtoValidator()
        {

            RuleFor(x => x.TravelDate).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.SeatCount).GreaterThan(0).WithMessage("Seat Count Should be Greater Then 0");
            RuleFor(x => x.ArrivalCityId).GreaterThan(0).WithMessage("Arrive City Id Should be Greater Then 0");
            RuleFor(x => x.DepartureCityId).GreaterThan(0).WithMessage("Departure City Id Should be Greater Then 0");
            RuleFor(x => x.ArrivalCityId).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.DepartureCityId).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            
        }


    }
}
