using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [Route("otp")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly OtpService _otpService;
        private readonly IMemoryCache _memoryCache;

        public OtpController(OtpService otpService, IMemoryCache memoryCache)
        {
            _otpService = otpService;
            _memoryCache = memoryCache;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendOtp(string email)
        {
            var otp = _otpService.GenerateOtp();

            // Store the OTP and email in the cache with a short expiration time
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5)); // Set expiration time as needed

            _memoryCache.Set(email, otp, cacheEntryOptions);

            await _otpService.SendOtpEmailAsync(email, otp);
            return Ok("OTP sent to your email.");
        }

        [HttpPost("verify")]
        public IActionResult VerifyOtp(string email, string otp)
        {
            if (_memoryCache.TryGetValue(email, out string storedOtp) && storedOtp == otp)
            {
                // OTP is correct, remove it from the cache
                _memoryCache.Remove(email);
                return Ok("OTP verified.");
            }

            return BadRequest("Invalid OTP.");
        }


    }
}

