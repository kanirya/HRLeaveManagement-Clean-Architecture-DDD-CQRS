using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.LeaveType.Validators
{
    public class UpdateLeaveTypeDtoValidator:AbstractValidator<LeaveTypeDto>
    {
        public UpdateLeaveTypeDtoValidator() { 
            Include(new ILeaveTypeDtoValidator());
            RuleFor(p => p.Id).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
