using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TPAdmissionTask.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace TPAdmissionTask.Controllers
{
    [Route("token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration? _configuration;
        private readonly DatabaseContext? _db;

        public TokenController(IConfiguration config, DatabaseContext context)
        {
            _configuration = config;
            _db = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserModel user)
        {
            if (user != null && user.Email != null && user.Password != null)
            {
                var uInfo = await GetUser(user.Email, user.Password);

                if (uInfo == null)
                    return BadRequest("Invalid credentials");

                if (_configuration == null)
                    return NotFound();

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId", uInfo.UserId.ToString()),
                    new Claim("Email", uInfo.Email!)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<UserModel?> GetUser(string email, string password)
        {
            if (_db!.Users == null)
                return null;

            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
