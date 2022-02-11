using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Helper;
using TodoListApi.InMemoryDbContext.Entities;

namespace TodoListApi.Interfaces
{
  public  interface ITodoItemService
    {
        dynamic GetTodoItemList(string userName);
        dynamic TodaysTodoItemList(string userName);
        List<TodoType> GetTodoItemTypes();
        ApiReturnObj AddTodoItem(TodoItem model);
        ApiReturnObj UpdateTodoItem(TodoItem model);
        ApiReturnObj DeleteTodoItem(int todoItemId);
    }
}
