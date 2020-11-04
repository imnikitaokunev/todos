using System.Collections.Generic;
using System.Threading.Tasks;
using Todos.Models;

namespace Todos.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetAsync();
        void Add(Todo todo);
        void Update(Todo todo);
        void Remove(Todo todo);
    }
}
