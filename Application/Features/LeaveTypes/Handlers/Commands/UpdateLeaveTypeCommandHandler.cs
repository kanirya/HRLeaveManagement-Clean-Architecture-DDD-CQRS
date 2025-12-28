using Application.DTOs.LeaveType.Validators;
using Application.Features.LeaveTypes.Requests.Commands;
using Application.Persistence.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LeaveTypes.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository=leaveTypeRepository;
            _mapper=mapper;
        }
        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator=new UpdateLeaveTypeDtoValidator();
            var validationResult=await validator.ValidateAsync(request.LeaveTypeDto);
            if (!validationResult.IsValid)
                throw new Exception();
            var leaveType=await _leaveTypeRepository.Get(request.LeaveTypeDto.Id);
            if (leaveType==null)
            {
                throw new Exception($"Leave type with id {request.LeaveTypeDto.Id} not found.");
            }
            _mapper.Map(request.LeaveTypeDto,leaveType);
            await _leaveTypeRepository.Update(leaveType);
            return Unit.Value;
        }
    }
}
