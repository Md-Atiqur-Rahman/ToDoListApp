using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListApi.InMemoryDbContext.Entities
{
    public class TodoType
    {
        [Key]
        public int TodoTypeId { get; set; }
        public string TypeName { get; set; }
    }
}
