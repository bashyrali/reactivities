using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Activities;
using Application.DTOs;
using FluentValidation;

namespace Application.Validator
{
    public class CreateActivityValidator : AbstractValidator<Create.Command>
    {
        public CreateActivityValidator()
        {
            RuleFor(x => x.ActivityDto.Title).NotEmpty();
            RuleFor(x => x.ActivityDto.Description).NotEmpty();
            RuleFor(x => x.ActivityDto.Date).NotEmpty();
            RuleFor(x => x.ActivityDto.Category).NotEmpty();
            RuleFor(x => x.ActivityDto.City).NotEmpty();
            RuleFor(x => x.ActivityDto.Venue).NotEmpty();
        }
    }
}
