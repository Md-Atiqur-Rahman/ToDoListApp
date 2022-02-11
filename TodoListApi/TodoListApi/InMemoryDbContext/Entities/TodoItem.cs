using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListApi.InMemoryDbContext.Entities
{
    public class TodoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TodoItemId { get; set; }
        public long TodoItemType { get; set; }
        public string TodoItemContent { get; set; }
        public DateTime? TodoItemDate { get; set; } = DateTime.Now;
        public string UserName { get; set; }
        public string ReminderDateTime { get; set; }
        public string DueDateTime { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
