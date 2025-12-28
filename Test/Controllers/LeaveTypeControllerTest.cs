using Application.Persistence.Contracts;
using AutoMapper;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controllers
{
    public  class LeaveTypeControllerTest
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        public LeaveTypeControllerTest()
        {
            _leaveTypeRepository= A.Fake<ILeaveTypeRepository>();
            _mapper=A.Fake<IMapper>();
        }
    }
}
