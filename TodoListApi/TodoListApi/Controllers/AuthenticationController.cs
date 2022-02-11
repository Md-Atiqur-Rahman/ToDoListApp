using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Helper;
using TodoListApi.InMemoryDbContext.Entities;
using TodoListApi.Interfaces;

namespace TodoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserInfo _userInfoService;
        ApiReturnObj returnObj = new ApiReturnObj();
        public AuthenticationController(IUserInfo userInfoService)
        {
            _userInfoService = userInfoService;
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(User model)
        {
            try
            {
                if (model==null)
                {
                    return BadRequest("NO Data is Found.");
                }
                var msg = _userInfoService.RegisterUser(model);
                returnObj.Message = msg;
                returnObj.IsExecute = true;
                returnObj.ApiData = msg;
                return Ok(returnObj);

            }
            catch (Exception ex)
            {

                returnObj.Message = ex.Message;
                returnObj.IsExecute = false;
                returnObj.ApiData = null;
                return Ok(returnObj);
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(User loginRequest)
        {
            try
            {
                if (loginRequest == null)
                {
                    if (string.IsNullOrEmpty(loginRequest.UserName))
                        return BadRequest("Missing UserName");
                    if (string.IsNullOrEmpty(loginRequest.Password))
                        return BadRequest("Missing Password");
                    
                }
                var loginResponse = _userInfoService.LogIn(loginRequest);

                if (loginResponse == null)
                {
                    return BadRequest($"Invalid credentials");
                }
                if (loginResponse != null)
                {
                    returnObj.IsExecute = true;
                    returnObj.ApiData = loginResponse;
                    return Ok(returnObj);
                }
                else
                {
                    returnObj.IsExecute = false;
                    returnObj.ApiData = null;
                    return Ok(returnObj);
                }
            }
            catch (Exception ex)
            {
                returnObj.Message = ex.Message;
                returnObj.IsExecute = false;
                returnObj.ApiData = null;
                return Ok(returnObj);
            }

        }
    }
}
