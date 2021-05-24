using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace list.data.Models
{
    [Table("TodoItems")]
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public string Creator { get; set; }
    }
}
