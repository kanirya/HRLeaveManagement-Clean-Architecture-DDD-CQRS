using Application.DTOs.LeaveType.Validators;
using Application.Exceptions;
using Application.Features.LeaveTypes.Requests.Commands;
using Application.Persistence.Contracts;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository,IMapper mapper) {
            _leaveTypeRepository=leaveTypeRepository;
            _mapper=mapper;
        }
        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator=new CreateLeaveTypeDtoValidator();
            var validationResult=await validator.ValidateAsync(request.LeaveTypeDto);
            if (!validationResult.IsValid) 
                throw new ValidationExceptions(validationResult);

            var leaveType=_mapper.Map<LeaveType>(request.LeaveTypeDto);
        leaveType=await _leaveTypeRepository.Add(leaveType);
            return leaveType.Id;
        }
    }
}
