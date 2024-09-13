using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using EmailService;

namespace WebAPI.Controllers
{
    [Route("email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(
            [FromForm] List<string> to,
            [FromForm] string subject,
            [FromForm] string content,
            [FromForm] IFormFileCollection attachments)
        {
            var message = new Message(to, subject, content, attachments);
            await _emailSender.SendEmailAsync(message);

            return Ok("Email sent successfully!");
        }

        private string GenerateOtp(int length = 6)
        {
            var random = new Random();
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                otp += random.Next(0, 10).ToString();
            }
            return otp;
        }



        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp(
            [FromForm] List<string> to,
         [FromForm] string subject = "Your OTP Code")
        {
            if (to == null || !to.Any())
            {
                return BadRequest("Recipient list cannot be empty.");
            }

            var otp = GenerateOtp();
            var content = $"Your OTP is: {otp}";

            // Log the OTP for debugging purposes (remove in production)
            Console.WriteLine($"Generated OTP: {otp}");

            var message = new Message(to, subject, content, null);
            await _emailSender.SendEmailAsync(message);

            return Ok("OTP sent successfully!");
        }


    }
}
