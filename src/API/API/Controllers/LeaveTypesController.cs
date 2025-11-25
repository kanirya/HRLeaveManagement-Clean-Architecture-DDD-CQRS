using Application.DTOs.LeaveType;
using Application.Exceptions;
using Application.Features.LeaveTypes.Requests.Commands;
using Application.Features.LeaveTypes.Requests.Queries;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0",Deprecated =true)]
    [ApiVersion("2.0")]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator)
        {
            _mediator=mediator;
        }



        [HttpGet]
        //[MapToApiVersion("1.0")] if there is no MapToApiVersion Allot to any action method this will work on every version
        public async Task<ActionResult<List<LeaveTypeDto>>> Get(CancellationToken cancellationToken)
        {
            Response.Headers.Add("Warning", "299 - 'API version 1.0 is deprecated, please migrate to 2.0'");
            var leaveType = await _mediator.Send(new GetLeaveTypeListRequest(),cancellationToken);

            return Ok(leaveType);
          
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<LeaveTypeDto>> Get(int id)
        {
            var leaveType=await _mediator.Send( new GetLeaveTypeDetailRequest{Id=id});
            return Ok(leaveType);
        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Post([FromBody] CreateLeaveTypeDto leaveTypeDto)
        {
            var command = new CreateLeaveTypeCommand { LeaveTypeDto=leaveTypeDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [MapToApiVersion("1.0")]
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
        [MapToApiVersion("1.0")]
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
