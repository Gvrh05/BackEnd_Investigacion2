using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static HackerRank1.DTO.AuthModel;

namespace HackerRank1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        [HttpPost("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserCredential user)
        {
            if (user.Email != "admin" || user.Password != "1234")
                return Unauthorized();

            var token = GenerateToken(user.Email);
            return Ok(new TokenResponse(token));
        }

        private string GenerateToken(string email)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, "admin")
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("mi-clave-secreta-super-segura-1234!")
            );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "MyApp",
                audience: "localhost:80",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
