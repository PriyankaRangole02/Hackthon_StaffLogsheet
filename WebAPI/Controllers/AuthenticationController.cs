//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace WebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthenticationController : Controller { 
    


   
//        private readonly IConfiguration _configuration;

//        public AuthController(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        [HttpPost("login")]
//        public IActionResult Login([FromBody] LoginModel model)
//        {
//            // Validate user credentials (this is just a placeholder, replace with your logic)
//            if (model.Username == "admin" && model.Password == "password")
//            {
//                var token = GenerateJwtToken(model.Username);
//                return Ok(new { token });
//            }
//            else
//            {
//                return Unauthorized();
//            }
//        }

//        private string GenerateJwtToken(string username)
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new[]
//                {
//                new Claim(ClaimTypes.Name, username)
//                // Additional claims can be added here
//            }),
//                Expires = DateTime.UtcNow.AddDays(1),
//                Issuer = _configuration["Jwt:Issuer"],
//                Audience = _configuration["Jwt:Audience"],
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//            };

//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            return tokenHandler.WriteToken(token);
//        }
//    }

//    public class LoginModel
//    {
//        public string Username { get; set; }
//        public string Password { get; set; }
//    }

//}

