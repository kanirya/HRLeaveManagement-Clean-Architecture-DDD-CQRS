using Application.DTOs.LeaveRequest.Validators;
using Application.Exceptions;
using Application.Features.LeaveRequests.Requests.Commands;
using Application.Persistence.Contracts;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveRequestRepository=leaveRequestRepository;
            _mapper=mapper;
            _leaveTypeRepository=leaveTypeRepository;
        }
        public  async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator=new CreateLeaveRequestDtoValidator(_leaveTypeRepository);

            var validationResult=await validator.ValidateAsync(request.CreateLeaveRequestDto);
            if(validationResult != null) {
                throw new ValidationExceptions(validationResult);
            }
            var leaveRequest=_mapper.Map<LeaveRequest>(request.CreateLeaveRequestDto);
            leaveRequest=await _leaveRequestRepository.Add(leaveRequest);
            if (leaveRequest==null)
            {
                throw new Exception("Error creating leave request");
            }
                return leaveRequest.Id;
        }
    }
}
