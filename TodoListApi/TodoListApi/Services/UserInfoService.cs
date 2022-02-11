using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TodoListApi.Helper;
using TodoListApi.InMemoryDbContext;
using TodoListApi.InMemoryDbContext.Entities;
using TodoListApi.Interfaces;

namespace TodoListApi.Services
{
    public class UserInfoService : IUserInfo
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string filePath;
        private readonly List<User> User = new List<User>();
        private readonly MemoryDbContext _context;
        public UserInfoService( MemoryDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            filePath= _hostingEnvironment.ContentRootPath + "\\jsonFile\\User.json";
            var jsonData = System.IO.File.ReadAllText(filePath);
            User = JsonConvert.DeserializeObject<List<User>>(jsonData) ?? new List<User>();
        }

        public string RegisterUser(User entity)
        {
            
            try
            {
                var salt = CommonHelper.GenerateSalt();
                var passwordHash = CommonHelper.HashUsingPbkdf2(entity.Password, salt);
                entity.PasswordSalt = salt;
                entity.HashPassword = passwordHash;
                _context.Add(entity);
                _context.SaveChanges();
                SaveDataInJson(entity);
                return ConstMessage.Register;
            }
            catch (System.Exception ex)
            {
                return ex.Message;

            }
            
        }


        public User GetUserInfoById(int userId)
        {
            
            return _context.Users.FirstOrDefault(x => x.UserId == userId);
        }

        public List<User> GetUserInformations()
        {
            //var jsonString = JsonConvert.SerializeObject(UserInformations);

            return _context.Users.ToList();
        }

        public object LogIn(User model)
        {
            var registerUser = _context.Users.FirstOrDefault(x => x.UserName == model.UserName && x.Password ==model.Password);
            if (registerUser == null)
                return null;
            var passwordHash = CommonHelper.HashUsingPbkdf2(model.Password, registerUser.PasswordSalt);

            if (registerUser.HashPassword != passwordHash)
                return null;
            var token = TokenHelper.GenerateToken(registerUser);
            var obj = new
            {
                Username = registerUser.UserName,
                Token = token
            };
            return obj;
        }
        private void SaveDataInJson(User user)
        {

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
                jsonObj.Add(user);
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);

            }
        }
    }
}
