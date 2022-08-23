using Microsoft.IdentityModel.Tokens;
using NoteApi.Models.Data;
using NoteApi.Utils.interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NoteApi.Utils
{
    public class Tokens : ITokens
    {
        public RefreshTokenData GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomNumber);
            return new RefreshTokenData
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(1),
                Created = DateTime.UtcNow
            };
        }

        public Guid DecryptToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("pCX02gaJHfPwdiovzkNjuREyTMjB2R")),
                ValidIssuer = "NoteAppServer",
                ValidAudience = "NoteAppUser",
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var value = jwtToken.Claims.First(x => x.Type == "id").Value;
            return Guid.Parse(value);
        }

        public string GenerateJwtToken(UserData user)
        {
            var claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
            };

            var expirationTime = DateTime.UtcNow.AddMinutes(15);
            var value = GenerateToken(
                "pCX02gaJHfPwdiovzkNjuREyTMjB2R",
                "NoteAppServer",
                "NoteAppUser",
                expirationTime,
                claims);

            return value;
        }

        private string GenerateToken(string secretKey, string issuer, string audience, DateTime utcExpirationTime,
            IEnumerable<Claim> claims = null)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                DateTime.UtcNow,
                utcExpirationTime,
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
