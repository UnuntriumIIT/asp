using System;
using list.data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace list.Services
{
    public interface ITodoListServices
    {
        Task AddAsync(TodoItem todoItem);

        Task<IEnumerable<TodoItem>> GetAllAsync(int page, int pageCount);

        Task<TodoItem> GetByIdAsync(Guid id);

        Task RemoveAsync(Guid id);

        Task EditAsync(TodoItem todoItem);

        Task ClearCompletedAsync();
    }
}
