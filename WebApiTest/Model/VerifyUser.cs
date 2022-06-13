using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;

namespace WebApiTest.Model
{
    public class VerifyUser
    {
        private readonly IConfiguration _configuration;

        public VerifyUser(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool Verify(LoginModel model)
        {
            if (model !=null)
            {
                if (model.LoginAccount=="jerry" && model.Password=="123")
                {
                    return true;
                }
            }
            return false;
        }

        public string GenerateToken(string loginAccount)
        {
            var issuer = _configuration.GetValue<string>("JwtSettings:Issuer");
            var sigatureKey = _configuration.GetValue<string>("JwtSettings:SignatureSecretKey");
            var expire = _configuration.GetValue<string>("JwtSettings:ExpireTime");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sigatureKey));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, loginAccount));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim("Dept", "Engineer"));
            claims.Add(new Claim("Role", "Manager"));

            var ClaimIdentity=new ClaimsIdentity(claims);

            var tokenDesc = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = issuer,
                //有效開始時間
                NotBefore = DateTime.Now,
                IssuedAt = DateTime.Now,
                Subject = ClaimIdentity,
                Expires = DateTime.Now.AddMinutes(double.Parse(expire)),
                SigningCredentials=credential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDesc);
            //序列化
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
