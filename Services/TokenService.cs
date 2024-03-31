using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using storeAPI.Interfaces;
using storeAPI.Models;

namespace storeAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<User> _userManager;
        public TokenService(IConfiguration config, UserManager<User> userManager)
        {   
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            _userManager = userManager;
        }

        public async Task<string> CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            // Lấy danh sách role của user và thêm chúng vào danh sách claim của token với key là "role" và value là tên role tương ứng.
            var roles = await _userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                // Thêm role vào danh sách claim của token với key là "role" và value là tên role tương ứng (Staff hoặc Customer)
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Lấy danh sách audience từ appsettings.json và tách chúng ra thành mảng. Ví dụ: "Staff,Customer" -> ["Staff", "Customer"]
            var audiences = _config["JWT:Audience"].Split(","); 
            foreach(var audience in audiences)
            {
                // Thêm mỗi audience vào danh sách claim của token với key là "aud" (audience) và value là audience tương ứng (Staff hoặc Customer)
                // Điều này giúp cho việc xác thực token dễ dàng hơn vì ta có thể xác định được token dành cho ai.
                claims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}