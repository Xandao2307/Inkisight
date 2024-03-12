using InkInsight.API.Configurations;
using InkInsight.API.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace InkInsight.API.Services
{
    public class TokenService
    {
        private JwtConfiguration _configuration;
        public TokenService() { }

        public TokenService(JwtConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken<T>(T obj)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = _configuration.JwtSecret;

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                SigningCredentials = credentials,
                Subject = GenerateClaims(obj)
            };

            var token = handler.CreateToken(tokenDescriptor);

            var strToken = handler.WriteToken(token);
            return strToken;
        }

        private static ClaimsIdentity GenerateClaims<T>(T obj)
        {
            var json = JsonSerializer.Serialize<T>(obj);
            var claim = new ClaimsIdentity();
            claim.AddClaim(new Claim("content", json));
            return claim;
        }
    }
}
