using System.Collections.Generic;
using System.Threading.Tasks;
using Todos.Models;

namespace Todos.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAsync();
        void Add(Todo todo);
        void Update(Todo todo);
        void Remove(Todo todo);
    }
}
