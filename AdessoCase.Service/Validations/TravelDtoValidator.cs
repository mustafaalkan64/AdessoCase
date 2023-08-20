using FluentValidation;
using AdessoCase.Core.DTOs;

namespace AdessoCase.Service.Validations
{
    public class TravelDtoValidator : AbstractValidator<TravelDto>
    {
        public TravelDtoValidator()
        {

            RuleFor(x => x.TravelDate).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.ArrivalCityId).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.DepartureCityId).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            
        }


    }
}
