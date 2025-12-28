using Application.DTOs.LeaveType;
using Application.Features.LeaveTypes.Requests.Commands;
using Application.Features.LeaveTypes.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator)
        {
            _mediator=mediator;
        }



        [HttpGet]
        
        public async Task<ActionResult<List<LeaveTypeDto>>> Get()
        {
            var leaveType = await _mediator.Send(new GetLeaveTypeListRequest());

            return Ok(leaveType);
          
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypeDto>> Get(int id)
        {
            var leaveType=await _mediator.Send( new GetLeaveTypeDetailRequest{Id=id});
            return Ok(leaveType);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateLeaveTypeDto leaveTypeDto)
        {
            var command = new CreateLeaveTypeCommand { LeaveTypeDto=leaveTypeDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
