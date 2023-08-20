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

            RuleFor(x => x.From).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.To).NotNull().WithMessage("{PropertyName} is required");
        }


    }
}
