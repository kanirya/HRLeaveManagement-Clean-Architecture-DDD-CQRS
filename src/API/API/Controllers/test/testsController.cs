using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.test
{
    [Route("api/[controller]")]
    [ApiController]
    public class testsController : ControllerBase
    {
        [HttpGet("test-error")]
        public IActionResult TestError()
        {
            throw new Exception("Something went wrong!");
        }


        [HttpGet("Text-Auth")]
        [Authorize]
        public IActionResult UnAuthorize()
        {
            return Ok("Jwt is working");
        }
    }
}
