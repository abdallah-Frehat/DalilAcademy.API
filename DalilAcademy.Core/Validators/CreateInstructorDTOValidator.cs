using DalilAcademy.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Validators
{
    public class CreateInstructorDTOValidator : AbstractValidator<CreateInstructorDTO>
    {
        public CreateInstructorDTOValidator()
        {
            RuleFor(x => x.FullName).NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty()
                .EmailAddress()
                .MaximumLength(200);

        }
    }
}
