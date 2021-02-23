using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/login")]//class route
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginContext _context;
        private readonly ApplicationSettings _appSettings;

        public LoginController(LoginContext context, IOptions<ApplicationSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("LoginUser")]
        public async Task<IActionResult> VerifyLogin(SpecificUserLogin userLogin)
        {
            var user = await _context.UserLogins.Where(x => x.userName == userLogin.userName).FirstOrDefaultAsync();

            if (user != null && user.password == userLogin.password)
            {

                if (user != null)
                {
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("userName",user.userName)

                    }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    return Ok(new { token });
                }

            }

            return BadRequest(new { message = "Username or password is incorrect" });
        }

        [HttpGet]
        [Authorize]
        [Route("VerifyCredential")]
        //GET  api/login/verifycredential
        public async Task<ActionResult> verifyCredential()
        {
            var userName = User.Claims.First(c => c.Type == "userName").Value;
            var user = await _context.UserLogins.Where(x => x.userName == userName).FirstOrDefaultAsync();

            return Ok(new
            {
                userName = user.userName,
                userType = user.userType
            });
        }
    }
}