using AdessoCase.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddessoCase.Service.Validations
{
    public class TravelFilterDtoValidator : AbstractValidator<TravelFilterDto>
    {
        public TravelFilterDtoValidator()
        {

            RuleFor(x => x.Departure).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Arrival).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
        }


    }
}
