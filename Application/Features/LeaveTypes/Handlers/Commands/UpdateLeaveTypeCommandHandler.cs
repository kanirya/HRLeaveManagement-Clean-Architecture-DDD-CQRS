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
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, int>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository=leaveTypeRepository;
            _mapper=mapper;
        }
        public async Task<int> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType=await _leaveTypeRepository.Get(request.Id);
            if (leaveType==null)
            {
                throw new Exception($"Leave type with id {request.Id} not found.");
            }
            var mappedLeaveType=_mapper.Map(request.LeaveTypeDto,leaveType);
            var updatedLeaveType=await _leaveTypeRepository.Update(mappedLeaveType);
            return updatedLeaveType.Id;
        }
    }
}
