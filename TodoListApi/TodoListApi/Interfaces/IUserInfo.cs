using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using TodoListApi.InMemoryDbContext.Entities;
using TodoListApi.Model;

namespace TodoListApi.Interfaces
{
    public interface IUserInfo
    {
        List<User> GetUserInformations();
        User GetUserInfoById(int UserId);
        string RegisterUser(User userInformation);
        object LogIn(User model);

        public static string GenerateSecureSecret()
        {
            var hmac = new HMACSHA256();
            return Convert.ToBase64String(hmac.Key);
        }

        //public static string GenerateToken(User user)
        //{
        //    bool isBlock = false;
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Convert.FromBase64String(Secret);

        //    var claimsIdentity = new ClaimsIdentity(new[] {
        //        new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString()),
        //        new Claim("IsBlocked", isBlock.ToString())
        //    });
        //    var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = claimsIdentity,
        //        Issuer = Issuer,
        //        Audience = Audience,
        //        Expires = DateTime.Now.AddDays(1),
        //        SigningCredentials = signingCredentials,

        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
    }
}
