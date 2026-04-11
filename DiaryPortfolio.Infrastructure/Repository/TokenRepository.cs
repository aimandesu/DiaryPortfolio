using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _symmetricSecurityKey;

        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"] ?? string.Empty)
            );
        }

        public TokenModel GenerateToken(
            string Email, 
            Guid UserId,
            Guid? PortfolioProfileId = null,
            Guid? DiaryProfileId = null)
        {
            var claims = new List<Claim>
            {
                new System.Security.Claims.Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, UserId.ToString()),
                new System.Security.Claims.Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, Email),
                new System.Security.Claims.Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (PortfolioProfileId.HasValue)
                claims.Add(new Claim("portfolio_profile_id", PortfolioProfileId.Value.ToString()));

            if (DiaryProfileId.HasValue)
                claims.Add(new Claim("diary_profile_id", DiaryProfileId.Value.ToString()));

            var creds = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["JwtSettings:ExpiryMinutes"])
            );

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var jwtToken = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);

            return new TokenModel
            {
                JWTAccessToken = jwtToken,
                RefreshToken = Guid.NewGuid().ToString(),
                ExpiresAt = expires
            };

        }
    }
}
