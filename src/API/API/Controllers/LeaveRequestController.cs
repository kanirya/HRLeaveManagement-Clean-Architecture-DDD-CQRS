using Application.DTOs.LeaveRequest;
using Application.Features.LeaveRequests.Requests.Commands;
using Application.Features.LeaveRequests.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestDto>>> Get()
        {
           var command=new GetLeaveRequestListRequest();
            var result=await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
          var command=new GetLeaveRequestDetailRequest {id=id};
            var result=await _mediator.Send(command);
            return Ok(result);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLeaveRequestDto leaveRequestDto)
        {
            var command=new CreateLeaveRequestCommand {CreateLeaveRequestDto=leaveRequestDto};
            var response =await _mediator.Send(command);
            return Ok(response);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveRequestDto updateLeaveRequestDto)
        {
            var command=new UpdateLeaveRequestCommand {Id=id,UpdateLeaveRequestDto=updateLeaveRequestDto};
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> ChangeApproval( [FromBody] ChangeLeaveRequestApprovalDto changeLeaveRequestApprovalDto)
        {
            var command = new UpdateLeaveRequestCommand { changeLeaveRequestDto=changeLeaveRequestApprovalDto };
            await _mediator.Send(command);
            return NoContent();
        }



        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command=new DeleteLeaveRequestCommand {Id=id};
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
