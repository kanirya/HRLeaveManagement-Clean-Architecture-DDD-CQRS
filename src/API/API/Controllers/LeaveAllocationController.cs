using Application.DTOs.LeaveAllocation;
using Application.Features.LeaveAllocation.Requests.Commands;
using Application.Features.LeaveAllocation.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase
    {
              private readonly IMediator _mediator;

        public LeaveAllocationController(IMediator mediator)
        {
            _mediator=mediator;
        }

        // GET: api/<LeaveAllocationController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
        {
            var command =new GetLeaveAllocationListRequest();
            var result=await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<LeaveAllocationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var command=new GetLeaveAllocationDetailRequest { Id = id };
            var result=await _mediator.Send(command);
            return Ok(result);
        }

        // POST api/<LeaveAllocationController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationDto leaveAllocationDto)
        {
            var command = new CreateLeaveAllocationCommand { CreateLeaveAllocationDto = leaveAllocationDto };
            var result =await _mediator.Send(command);
            return Ok(result);
        }

        // PUT api/<LeaveAllocationController>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationDto updateLeaveAllocationDto)
        {
            var command=new UpdateLeaveAllocationCommand { UpdateLeaveAllocationDto = updateLeaveAllocationDto };
            await _mediator.Send(command);
            return NoContent();

        }

        // DELETE api/<LeaveAllocationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command=new DeleteLeaveAllocationCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
