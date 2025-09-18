using Application.DTOs.LeaveAllocation.Validators;
using Application.Features.LeaveAllocation.Requests.Commands;
using Application.Persistence.Contracts;
using Application.Responses;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LeaveAllocation.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>
    {
          private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveAllocationRepository=leaveAllocationRepository;
            _mapper=mapper;

            _leaveTypeRepository=leaveTypeRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        { 
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveAllocationDtoValidator(_leaveTypeRepository);

            var validationResult = await validator.ValidateAsync(request.CreateLeaveAllocationDto);
            if(!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed ";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }

            var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request.CreateLeaveAllocationDto);
            leaveAllocation = await _leaveAllocationRepository.Add(leaveAllocation);
            if(leaveAllocation==null)
            {
                response.Success = false;
                response.Message = "Creation Failed ";
                return response;


            }
            response.Success = true;
            response.Message = "Creation Successful ";
            response.Id = leaveAllocation.Id;
            return response;
        }
    }
}
