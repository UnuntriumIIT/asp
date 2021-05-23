using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using list.data;
using list.data.Models;

namespace list.Services
{
    public class TodoListServices : ITodoListServices
    {
        private readonly TodoListDbContext _context;

        public TodoListServices(TodoListDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TodoItem todoItem)
        {
            await _context.TodoItems.AddAsync(todoItem);
            await _context.SaveChangesAsync();
        }

        public Task ClearCompletedAsync()
        {
            var completed = _context.TodoItems.Where(x => x.IsCompleted);

            _context.RemoveRange(completed);

            _context.SaveChangesAsync().Wait();
            return Task.CompletedTask;
        }

        public async Task EditAsync(TodoItem todoItem)
        {
            TodoItem item = _context.TodoItems.SingleOrDefault(x => x.Id == todoItem.Id);

            if(item != null)
            {
                item.IsCompleted = todoItem.IsCompleted;
                item.Title = todoItem.Title;
                _context.TodoItems.Update(item);
                await _context.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<TodoItem>> GetAllAsync(int page, int pageCount)
        {
            IEnumerable<TodoItem> result = _context.TodoItems.ToList();

            result = result.Skip(page * pageCount).Take(pageCount);

            return Task.FromResult(result);
        }

        public Task<TodoItem> GetByIdAsync(Guid id)
        {
            TodoItem item = _context.TodoItems.SingleOrDefault(x => x.Id == id);

            return Task.FromResult(item);
        }

        public async Task RemoveAsync(Guid id)
        {
            TodoItem item = _context.TodoItems.SingleOrDefault(x => x.Id == id);

            if(item != null)
            {
                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
