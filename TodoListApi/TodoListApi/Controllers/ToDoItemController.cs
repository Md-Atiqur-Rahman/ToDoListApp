using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoListApi.Helper;
using TodoListApi.InMemoryDbContext.Entities;
using TodoListApi.Interfaces;

namespace TodoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly ITodoItemService _iTodoItemService;
        ApiReturnObj returnObj = new ApiReturnObj();
        public ToDoItemController(ITodoItemService iTodoItemService)
        {
            _iTodoItemService = iTodoItemService;
        }
        [HttpGet("getToDoItemTypes")]
        [Authorize(Policy = "OnlyNonBlockedUser")]
        public IActionResult GetToDoItemTypes()
        {
            try
            {
                var data = _iTodoItemService.GetTodoItemTypes();
                if (data.Any())
                {
                    returnObj.IsExecute = true;
                    returnObj.ApiData = data;
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
                returnObj.IsExecute = false;
                returnObj.Message = ex.Message;
                returnObj.ApiData = null;
                return Ok(returnObj);
            }

        }

        [HttpGet("get-all")]
        [Authorize(Policy = "OnlyNonBlockedUser")]
        public IActionResult GetAll()
        {
            try
            {
                var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                string userName = claim.Value;
                //string userName = "titu";
                var data = _iTodoItemService.GetTodoItemList(userName);
                if (data != null)
                {
                    returnObj.IsExecute = true;
                    returnObj.ApiData = data;
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
                returnObj.IsExecute = false;
                returnObj.Message = ex.Message;
                returnObj.ApiData = null;
                return Ok(returnObj);
            }

        }
        [HttpGet("get-todaysTodoItemList")]
        [Authorize(Policy = "OnlyNonBlockedUser")]
        public IActionResult GetTodaysTodoItemList()
        {
            try
            {
                var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                string userName = claim.Value;
                //string userName = "titu";
                var data = _iTodoItemService.TodaysTodoItemList(userName);
                if (data != null)
                {
                    returnObj.IsExecute = true;
                    returnObj.ApiData = data;
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
                returnObj.IsExecute = false;
                returnObj.Message = ex.Message;
                returnObj.ApiData = null;
                return Ok(returnObj);
            }

        }

        [HttpPost]
        [Route("add")]
        [Authorize(Policy = "OnlyNonBlockedUser")]
        public IActionResult Add(TodoItem model)
        {

            try
            {
                returnObj = _iTodoItemService.AddTodoItem(model);
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
        [Route("update")]
        [Authorize(Policy = "OnlyNonBlockedUser")]
        public IActionResult Update(TodoItem model)
        {

            try
            {
                var returnObj = _iTodoItemService.UpdateTodoItem( model);
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

        [HttpGet("delete/{todoItem}")]
        [Authorize(Policy = "OnlyNonBlockedUser")]
        public IActionResult Delete(int todoItem)
        {
            try
            {
                returnObj = _iTodoItemService.DeleteTodoItem(todoItem);
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

       

    }
}
