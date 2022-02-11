using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.InMemoryDbContext.Entities;

namespace TodoListApi.InMemoryDbContext
{
    public class MemoryDbContext:DbContext
    {
        public MemoryDbContext(DbContextOptions options):base (options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<TodoType> TodoTypes { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
