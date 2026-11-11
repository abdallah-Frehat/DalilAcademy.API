using DalilAcademy.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Validators
{
    public class CreateCourseDTOValidator : AbstractValidator<CreateCourseDTO>
    {
        public CreateCourseDTOValidator()
        {
            RuleFor(x=>x.Price).GreaterThanOrEqualTo(0)
                .WithMessage("Price must be 0 or more");
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(x => x.InstructorId).GreaterThan(0)
                .WithMessage("InstructorId must be greater than 0");


        }
    }
}
