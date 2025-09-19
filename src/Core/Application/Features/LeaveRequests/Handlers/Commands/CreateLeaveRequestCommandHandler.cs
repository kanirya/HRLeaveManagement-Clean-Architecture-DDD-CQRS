using Application.Contracts.Infrastructure;
using Application.DTOs.LeaveRequest.Validators;
using Application.Exceptions;
using Application.Features.LeaveRequests.Requests.Commands;
using Application.Models;
using Application.Persistence.Contracts;
using Application.Responses;
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
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IEmailSender _emailSender;
        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IEmailSender emailSender)
        {
            _leaveRequestRepository=leaveRequestRepository;
            _mapper=mapper;
            _leaveTypeRepository=leaveTypeRepository;
            _emailSender=emailSender;
        }
        public  async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator=new CreateLeaveRequestDtoValidator(_leaveTypeRepository);

            var validationResult=await validator.ValidateAsync(request.CreateLeaveRequestDto);

            if (!validationResult.IsValid) {
                response.Success = false;
                response.Message = "Creation Failed ";
               response.Errors=validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }
            
            var leaveRequest=_mapper.Map<LeaveRequest>(request.CreateLeaveRequestDto);

            leaveRequest=await _leaveRequestRepository.Add(leaveRequest);
            if (leaveRequest==null)
            {
                response.Success = false;
                response.Message = "Creation Failed ";
                return response;
            }
            response.Success = true;
            response.Message = "Creation Successful ";
            response.Id = leaveRequest.Id;

            var email=new Email
            {
                To="user@localhost",
                Subject="Leave Request Created",
                Body=$"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been created successfully."
            };


            try
            {
                await _emailSender.SendEmail(email);

            }
            catch (Exception ex)
            {
                //log or handle error, but don't throw
            }
            return response;
        }
    }
}
