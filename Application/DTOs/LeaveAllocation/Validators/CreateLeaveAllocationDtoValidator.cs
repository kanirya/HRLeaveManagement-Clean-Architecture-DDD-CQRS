using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.LeaveAllocation.Validators
{
    public class CreateLeaveAllocationDtoValidator:AbstractValidator<CreateLeaveAllocationDto>
    {
        public CreateLeaveAllocationDtoValidator() { 
            RuleFor(q=>q.NumberOfDays).GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .LessThan(100).WithMessage("{PropertyName} must be less than {ComparisonValue}.");
            RuleFor(q => q.LeaveTypeId).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
            RuleFor(q => q.Period).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().GreaterThan(100).WithMessage("{PropertyName} must be greater than {ComparisonValue}")
                .LessThan(3000).WithMessage("{PropertyName} must be less than {ComparisonValue}");


        }
    }
}
