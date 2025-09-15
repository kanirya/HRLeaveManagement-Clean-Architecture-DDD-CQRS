using Application.Persistence.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.LeaveAllocation.Validators
{
    public class ILeaveAllocationDtoValidator:AbstractValidator<ILeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public ILeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository=leaveTypeRepository;
           RuleFor(p=>p.NumberOfDays).NotNull().NotEmpty().WithMessage("{PropertyName} is Required")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
            RuleFor(p => p.LeaveTypeId)
              .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
              .MustAsync(async (id, token) =>
              {
                  var leaveType = await _leaveTypeRepository.Exists(id);
                  return leaveType;
              }).WithMessage("{PropertyName} does not exist.");
        }

      
    }
}
