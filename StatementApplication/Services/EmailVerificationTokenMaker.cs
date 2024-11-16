using Microsoft.IdentityModel.Tokens;
using StatementApplication.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StatementApplication.Services
{
    public class EmailVerificationTokenMaker
    {
        public string GenerateEmailVerificationToken(Student student, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, student.StudentId.ToString()),
                        new Claim(ClaimTypes.Email, student.Email)
                    }),
                Expires = DateTime.UtcNow.AddHours(24), // Token expires in 24 hours
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
