using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListApi.InMemoryDbContext.Entities
{
    public class User
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EMail { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string HashPassword { get; set; }
    }
}
