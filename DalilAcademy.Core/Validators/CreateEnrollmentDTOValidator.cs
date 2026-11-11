using DalilAcademy.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Validators
{
    public class CreateEnrollmentDTOValidator : AbstractValidator<CreateEnrollmentDTO>
    {
        public CreateEnrollmentDTOValidator()
        {
            RuleFor(x => x.StudentId).GreaterThan(0);
            RuleFor(x => x.CourseId).GreaterThan(0);
        }
    }
}
