using Application.Persistence.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.LeaveRequest.Validators
{
    public class CreateLeaveRequestDtoValidator:AbstractValidator<CreateLeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository=leaveTypeRepository;

          Include(new ILeaveRequestDtoValidator(_leaveTypeRepository));
            RuleFor(x => x).NotNull().WithMessage("Leave request cannot be null");
            RuleFor(q => q.RequestComments)
                .MaximumLength(300).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
          
        }
    }
}
