
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XBlog.Models.Entities;

namespace Xblog.Data.Implementation
{
    public class JwtAuthenticator
    {
        private readonly JwtConfiguration _jwtConfig;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public JwtAuthenticator(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, JwtConfiguration jwtConfig)
        {
            _jwtConfig = jwtConfig;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Jwt> GenerateJwtToken(User user)
        {
            var currentDateTime = DateTime.Now;
            var claims = await GetValidClaims(user, currentDateTime);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.ServerSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = currentDateTime.AddDays(Convert.ToDouble(_jwtConfig.ExpiresIn));

            var token = new JwtSecurityToken(
                _jwtConfig.Issuer,
                _jwtConfig.Audience,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new Jwt
            {
                AccessToken = encodedJwt,
                Issued = currentDateTime,
                Expires = expires,
            };
        }

        private async Task<List<Claim>> GetValidClaims(User user, DateTime dateIssued)
        {
            IdentityOptions _options = new IdentityOptions();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(dateIssued).ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var userClaims = await _userManager.GetClaimsAsync(user);

            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (Claim roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }
            return claims;
        }

        private object ToUnixEpochDate(DateTime dateIssued)
        {
            var timestamp = (dateIssued - new DateTime(1970, 01, 01)).TotalMilliseconds;
            return timestamp;
        }
    }

    public class Jwt
    {
        public string AccessToken { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expires { get; set; }
    }
}
