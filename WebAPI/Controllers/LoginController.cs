using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.DatabaseContext;

namespace WebAPI.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private ProjectDbContext _db;
        public LoginController(IConfiguration config, ProjectDbContext db)
        {
            _config = config;
                _db = db;
 }

        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest loginRequest)

        {

           
            var user = _db.users.Where(u => u.Email == loginRequest.UserName && u.Password == loginRequest.Password).Include(u => u.GetRole).FirstOrDefault();
            if (user != null)
            {
                var claims = new[]
               {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.GetRole.RoleName) 
            // Assuming RoleName is the role description
               };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  
                  claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
                

                return Ok(new { name = user.UserName, role = user.GetRole.RoleName,id=user.UserId, token });
            }
            else
            {
                return Ok(new { error = "Invalid Username or Password" });
            }
        }
    }

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

}

