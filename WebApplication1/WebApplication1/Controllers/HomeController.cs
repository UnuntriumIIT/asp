using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using list.Services;
using list.data.Models;
using list.Web.Models;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Models;

namespace list.Web.Controllers
{
    [Authorize]
    [Controller]
    public class HomeController : Controller
    {
        private readonly ITodoListServices _services;

        public HomeController(ITodoListServices services)
        {
            _services = services;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(bool? completed)
        {
            var todoItems = await
            _services.GetAllAsync(0, 100);

            var items = ToViewModels(todoItems);

            if (completed != null)
            {
                items = items.Where(x => x.IsCompleted == completed);
            }

            return View(items);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {

            TodoItemCreateModel model = new TodoItemCreateModel();
            if (User.Identity.IsAuthenticated)
                return View(model);
            else return Content("Вы должны войти!");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(TodoItemCreateModel todoItem)
        {
            if (User.Identity.IsAuthenticated)
            {
                var item = ToTodoItem(todoItem);
                item.Creator = User.Identity.Name;

                _services.AddAsync(item).Wait();
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var todoItem = await _services.GetByIdAsync(id);

            if (todoItem != null)
            {
                var model = ToEditModel(todoItem);

                return View(model);
            }

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(TodoItemEditModel model)
        {
            var todoItem = await _services.GetByIdAsync(model.Id);

            todoItem.Title = model.Title;
            todoItem.IsCompleted = model.IsCompleted;

            _services.EditAsync(todoItem).Wait();

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _services.RemoveAsync(id);

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult ClearCompleted()
        {
            _services.ClearCompletedAsync().Wait();

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        private TodoItem ToTodoItem(TodoItemCreateModel model)
        {
            var todoItem = new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                IsCompleted = model.IsCompleted,
                Creator = User.Identity.Name
        };

            return todoItem;
        }

        [AllowAnonymous]
        private IEnumerable<TodoItemViewModel> ToViewModels(IEnumerable<TodoItem> collention)
        {
            IEnumerable<TodoItemViewModel> items = collention.Select(x =>
                new TodoItemViewModel
                {
                    Id = x.Id,
                    IsCompleted = x.IsCompleted,
                    Title = x.Title,
                    Creator = x.Creator
                }
            );

            return items;
        }

        [Authorize]
        private TodoItemEditModel ToEditModel(TodoItem item)
        {
            var model = new TodoItemEditModel
            {
                Id = item.Id,
                Title = item.Title,
                IsCompleted = item.IsCompleted
        };

            return model;
        }
    }
}
