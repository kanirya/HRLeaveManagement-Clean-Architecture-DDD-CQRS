using Application.Exceptions;
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
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository=leaveTypeRepository;
            _mapper=mapper;
        }
        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType=await _leaveTypeRepository.Get(request.Id);
            if (leaveType==null)
            {
                throw new NotFoundException(nameof(leaveType),request.Id);
            }

            await _leaveTypeRepository.Delete(leaveType);
            return Unit.Value;
        }
    }
}
