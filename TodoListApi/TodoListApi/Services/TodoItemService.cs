using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Helper;
using TodoListApi.InMemoryDbContext;
using TodoListApi.InMemoryDbContext.Entities;
using TodoListApi.Interfaces;

namespace TodoListApi.Services
{
    public class TodoItemService : ITodoItemService
    {

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string filePath;
       
        private readonly MemoryDbContext _context;
        ApiReturnObj returnObj = new ApiReturnObj();
        public TodoItemService(MemoryDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            filePath = _hostingEnvironment.ContentRootPath + "\\jsonFile\\TodoItem.json";
           
        }
        
        public ApiReturnObj AddTodoItem(TodoItem model)
        {
            ApiReturnObj response = new ApiReturnObj();
            try
            {
                model.TodoItemDate = DateTime.Now;
                _context.Add(model);
                _context.SaveChanges();
                SaveDataInJson(model);
                response.IsExecute = true;
                response.Message = ConstMessage.Inserted;
                response.ApiData = null;
                return response;
            }
            catch (Exception ex)
            {
                response.IsExecute = false;
                response.Message = ex.Message;
                response.ApiData = null;
                return response;
            }
            
        }

        public ApiReturnObj DeleteTodoItem(int todoItemId)
        {
            try
            {
                var data = _context.TodoItems.FirstOrDefault(x=>x.TodoItemId==todoItemId);
                _context.Remove(data);
                _context.SaveChanges();
                DeleteJson(todoItemId);
                returnObj.IsExecute = true;
                returnObj.Message = ConstMessage.Deleted;
                returnObj.ApiData = null;
                return returnObj;
            }
            catch (Exception ex)
            {

                returnObj.IsExecute = false;
                returnObj.Message = ex.Message;
                returnObj.ApiData = null;
                return returnObj;
            }
        }

        public dynamic GetTodoItemList(string userName)
        {

            var data = (from t in _context.TodoItems.Where(x => x.UserName == userName)
                        join ty in _context.TodoTypes on t.TodoItemType equals ty.TodoTypeId
                        select new
                        {
                            TodoItemId=t.TodoItemId,
                            TodoItemType= t.TodoItemType,
                            TodoItemTypeName= ty.TypeName,
                            TodoItemContent= t.TodoItemContent,
                            IsCompleted=t.IsCompleted,
                            DueDateTime = t.DueDateTime??null,
                            ReminderDateTime = t.ReminderDateTime ?? null,
                            TodoItemDate = t.TodoItemDate ?? null,
                        }).ToList();
            return data;
        }

        public List<TodoType> GetTodoItemTypes()
        {
            return _context.TodoTypes.ToList();
        }

        public ApiReturnObj UpdateTodoItem(TodoItem model)
        {
            
            try
            {
                var todoItem = _context.TodoItems.FirstOrDefault(x => x.TodoItemId == model.TodoItemId);
                if (todoItem != null)
                {
                    if (model.TodoItemType > 0)
                        todoItem.TodoItemType = model.TodoItemType;
                    if (!string.IsNullOrEmpty(model.TodoItemContent))
                        todoItem.TodoItemContent = model.TodoItemContent;
                    todoItem.IsCompleted = model.IsCompleted;
                    todoItem.ReminderDateTime = model.ReminderDateTime;
                    todoItem.DueDateTime = model.DueDateTime;
                    todoItem.TodoItemDate = DateTime.Now;

                    _context.TodoItems.Update(todoItem);
                    _context.SaveChanges();

                    UpdateJson(todoItem);
                }
                returnObj.IsExecute = true;
                returnObj.Message = ConstMessage.Updated;
                returnObj.ApiData = null;
                return returnObj;
            }
            catch (Exception ex)
            {

                returnObj.IsExecute = false;
                returnObj.Message = ex.Message;
                returnObj.ApiData = null;
                return returnObj;
            }
           
        }
        private void SaveDataInJson(TodoItem todoItem)
        {
            
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TodoItem>>(json) ?? new List<TodoItem>();
                jsonObj.Add(todoItem);
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);

            }
        }
        public void DeleteJson(int todoItemId)
        {
            
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TodoItem>>(json) ?? new List<TodoItem>();
                    foreach (var item in jsonObj)
                    {
                        if (item.TodoItemId == todoItemId)
                        {
                            jsonObj.Remove(item);
                            break;
                        }

                    }
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(filePath, output);
                }

            }
            catch (Exception)
            {
                throw new ArgumentException("User has not deleted");
            }
        }
       
        private void UpdateJson(TodoItem todoItem)
        {
            
            try
            {
                string json = File.ReadAllText(filePath);
                var jsonObj =  Newtonsoft.Json.JsonConvert.DeserializeObject<List<TodoItem>>(json) ?? new List<TodoItem>();
                foreach (var item in jsonObj)
                {
                    if(item.TodoItemId == todoItem.TodoItemId)
                    {
                        item.TodoItemContent = "test";
                        break;
                    }

                }
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);
                
            }
            catch (Exception ex)
            {

                
            }
        }
        public dynamic TodaysTodoItemList(string userName)
        {
            DateTime todaysDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 01, 00);

            var data = (from t in _context.TodoItems.Where(x => x.UserName == userName && x.TodoItemDate > todaysDate && (x.TodoItemType==2|| x.TodoItemType==3))
                        join ty in _context.TodoTypes on t.TodoItemType equals ty.TodoTypeId
                        select new
                        {
                            TodoItemId = t.TodoItemId,
                            TodoItemType = t.TodoItemType,
                            TodoItemTypeName = ty.TypeName,
                            TodoItemContent = t.TodoItemContent,
                            IsCompleted = t.IsCompleted,
                            DueDateTime = t.DueDateTime ?? null,
                            ReminderDateTime = t.ReminderDateTime ?? null,
                            TodoItemDate = t.TodoItemDate ?? null,
                        }).ToList();
            return data;
        }


    }
}
