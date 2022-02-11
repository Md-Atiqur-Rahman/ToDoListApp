using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TodoListApi.InMemoryDbContext.Entities;
using TodoListApi.Interfaces;

namespace TodoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
       private readonly IUserInfo _userInfoService;
       
        //private readonly IHostingEnvironment _hostingEnvironment;
        public UserInfoController(IUserInfo userInfoService)
        {
            
            _userInfoService = userInfoService;
           // _hostingEnvironment = hostingEnvironment;
        }
       
        [HttpGet]
        [Route("GetUserInformations")]
        public ActionResult<IEnumerable<User>> GetUserInformations()
        {
            
            return _userInfoService.GetUserInformations();
        }
        [HttpGet]
        [Route("GetUserInfoById")]
        public ActionResult<User> GetUserInfoById(int id)
        {
            return _userInfoService.GetUserInfoById(id);
        }
        [HttpPost]
        [Route("Add")]
        public ActionResult<IEnumerable<User>> Add(User userInformation)
        {
            //string contentRootPath = _hostingEnvironment.ContentRootPath + "\\jsonFile\\User.json";
            var isSave = _userInfoService.RegisterUser(userInformation);
            return _userInfoService.GetUserInformations();
        }
    }
}
