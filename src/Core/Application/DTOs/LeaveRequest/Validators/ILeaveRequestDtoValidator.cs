using Application.Persistence.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.LeaveRequest.Validators
{
    public class ILeaveRequestDtoValidator:AbstractValidator<ILeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public ILeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository) { 
             _leaveTypeRepository = leaveTypeRepository;
            RuleFor(p=>p.StartDate)
                .LessThan(p => p.EndDate).WithMessage("{PropertyName} must be before End Date.");
            RuleFor(p => p.EndDate)
                .GreaterThan(p => p.StartDate).WithMessage("{PropertyName} must be after Start Date.");
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
