using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace list.Services
{
    public static class TodoListServicesExtensions
    {
        public static IServiceCollection AddTodoList(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddScoped<ITodoListServices, TodoListServices>();
        }
    }
}
