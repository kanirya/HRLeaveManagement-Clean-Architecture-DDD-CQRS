using Application.Contracts.Infrastructure;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.test
{
    [Route("api/[controller]")]
    [ApiController]
    public class testsController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        public testsController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
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


        [HttpGet("send-otp")]
        public async Task<IActionResult> SendOtp([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required.");

            // Generate a 6-digit OTP
            var otp = new Random().Next(100000, 999999).ToString();

            // Create professional HTML body
            var body = $@"
        <html>
        <body style='font-family: Arial, sans-serif; background-color:#f9f9f9; padding:20px;'>
            <div style='max-width:600px; margin:auto; background:#fff; border-radius:8px; box-shadow:0 2px 10px rgba(0,0,0,0.1); padding:30px;'>
                <h2 style='color:#333;'>Your OTP Code</h2>
                <p style='font-size:16px; color:#555;'>Hello,</p>
                <p style='font-size:16px; color:#555;'>Use the OTP below to verify your email address:</p>
                <div style='text-align:center; margin:30px 0;'>
                    <span style='display:inline-block; background:#007bff; color:#fff; font-size:32px; letter-spacing:4px; padding:15px 30px; border-radius:8px; font-weight:bold;'>{otp}</span>
                </div>
                <p style='font-size:14px; color:#888;'>This code will expire in 5 minutes. Please do not share it with anyone.</p>
                <hr style='border:none; border-top:1px solid #eee; margin:20px 0;'/>
                <p style='font-size:12px; color:#aaa;'>© {DateTime.UtcNow.Year} Vexora. All rights reserved.</p>
            </div>
        </body>
        </html>
    ";

            var message = new Application.Models.Email
            {
                To = email,
                Subject = "Your OTP Verification Code",
                Body = body
            };

            bool result = await _emailSender.SendEmail(message);

            if (result)
                return Ok(new { success = true, message = "OTP sent successfully", otp }); // return otp here only for testing

            return StatusCode(500, new { success = false, message = "Failed to send OTP" });
        }
    }


    }
