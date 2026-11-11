using DalilAcademy.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DalilAcademy.Core.Validators
{
    public class CreateStudentDTOValidator : AbstractValidator<CreateStudentDTO>
    {
       public CreateStudentDTOValidator()
        {
            RuleFor(x => x.FullName).NotEmpty()
                .MaximumLength(200);    
            RuleFor(x => x.Email).NotEmpty()
                .EmailAddress()
                .MaximumLength(200);
            RuleFor(x => x.PhoneNumber).MaximumLength(15);
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
        }

    }
}
