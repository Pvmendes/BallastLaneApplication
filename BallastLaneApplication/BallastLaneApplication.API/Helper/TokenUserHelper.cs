using BallastLaneApplication.Core.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace BallastLaneApplication.API.Helper
{
    public static class TokenUserHelper
    {
        public static string GetUserFromAccessToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var username = securityToken.Claims.First(claim => claim.Type == "sub").Value;

            return username;
        }

        public static string GetUserFromHttpContext(HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            return GetUserFromAccessToken(token);
        }
    }
}
