using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace list.Web.Models
{
    public class TodoItemViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}