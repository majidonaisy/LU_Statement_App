using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using StatementApplication.Configs;
using StatementApplication.Data;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StatementApplication.Controllers
{
    [Route("/verify")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        AppDataContext _context;
        SecretKey _key;
        public VerificationController(AppDataContext context,SecretKey secretkey)
        {
            _context = context;
            _key = secretkey;
        }


        [HttpGet("")]
        public IActionResult VerifyEmail([FromQuery] string token)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key.secretKey);

            try
            {
                tokenhandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedtoken);

                var jwttoken = (JwtSecurityToken)validatedtoken;
                var useremail = jwttoken.Claims.FirstOrDefault(x => x.Type == "email").Value;
                var user = _context.Students.FirstOrDefault(x => x.Email == useremail);
                if (user == null || user.Verified)
                {
                    return BadRequest();
                }
                user.Verified = true;
                _context.SaveChanges();
                return Ok("verification successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }






        [HttpGet("check")]
        public string CheckEndpoint([FromQuery]string token)
        {

            return token;
        }
    }
}
