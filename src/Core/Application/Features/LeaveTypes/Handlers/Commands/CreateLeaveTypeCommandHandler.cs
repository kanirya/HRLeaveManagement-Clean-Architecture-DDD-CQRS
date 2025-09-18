using Application.DTOs.LeaveType.Validators;
using Application.Exceptions;
using Application.Features.LeaveTypes.Requests.Commands;
using Application.Persistence.Contracts;
using Application.Responses;
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
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository,IMapper mapper) {
            _leaveTypeRepository=leaveTypeRepository;
            _mapper=mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response=new BaseCommandResponse();

            var validator=new CreateLeaveTypeDtoValidator();
            var validationResult=await validator.ValidateAsync(request.LeaveTypeDto);
            if (!validationResult.IsValid)
            {
                response.Success=false;
                response.Message="Creation Failed";
                response.Errors=validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }
               

             var leaveType=_mapper.Map<LeaveType>(request.LeaveTypeDto);
              leaveType=await _leaveTypeRepository.Add(leaveType);
            response.Success=true;
            response.Message="Creation Successful";
            response.Id=leaveType.Id;
            return response;
        }
    }
}
