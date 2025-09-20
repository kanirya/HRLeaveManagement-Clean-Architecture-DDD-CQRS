using Application.DTOs.LeaveType;
using Application.Exceptions;
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
        
        public async Task<ActionResult<List<LeaveTypeDto>>> Get(CancellationToken cancellationToken)
        {
            var leaveType = await _mediator.Send(new GetLeaveTypeListRequest(),cancellationToken);

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

        [HttpPut]
        public async Task<ActionResult> Put( [FromBody] LeaveTypeDto leaveTypeDto)
        {
            try
            {
                var command = new UpdateLeaveTypeCommand { LeaveTypeDto = leaveTypeDto };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message); // 404
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message); // 400
            }
            }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new DeleteLeaveTypeCommand { Id=id };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message); // 404
            }

        }

      

    }
}
