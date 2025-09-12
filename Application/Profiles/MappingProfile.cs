using Application.DTOs;
using Application.DTOs.LeaveAllocation;
using Application.DTOs.LeaveRequest;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<LeaveRequest,LeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequest,LeaveRequestListDto>().ReverseMap();
            CreateMap<LeaveType,LeaveTypeDto>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationDto>().ReverseMap();
        }
    }
}
