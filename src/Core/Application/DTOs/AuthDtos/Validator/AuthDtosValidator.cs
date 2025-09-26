using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AuthDtos.Validator
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(q => q.Name)
    .NotEmpty().WithMessage("{PropertyName} is required")
    .NotNull().WithMessage("{PropertyName} is required")
    .MaximumLength(50).WithMessage("{PropertyName} must not exceed {MaxLength} characters.")
    .Matches(@"^[A-Za-z]+$").WithMessage("{PropertyName} must only contain letters.");

            RuleFor(q => q.Email)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .EmailAddress().WithMessage("{PropertyName} must be a valid email address.");

            RuleFor(q => q.Password)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} is required")
                .MinimumLength(6).WithMessage("{PropertyName} must be at least {MinLength} characters long.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed {MaxLength} characters.")
                .Matches(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?""{}|<>]).+$")
                .WithMessage("{PropertyName} must contain at least one uppercase letter, one number, and one special character.");

        }

    }

    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(q => q.Email)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .EmailAddress().WithMessage("{PropertyName} must be a valid email address.");
            RuleFor(q => q.Password)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
