using FluentValidation;

namespace DalilAcademy.Core.DTOs
{
    public class RegisterResponseDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterResponseDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.");
        }
    }
}