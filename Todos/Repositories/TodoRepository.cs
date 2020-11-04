using System.Collections.Generic;
using System.Threading.Tasks;
using Todos.Models;

namespace Todos.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DbContext _context;

        public TodoRepository(DbContext context) => _context = context;

        public Task<IEnumerable<Todo>> GetAsync() => Task.FromResult(_context.Todos);

        public void Add(Todo todo) => _context.Add(todo);

        public void Update(Todo todo) => _context.Update(todo);

        public void Remove(Todo todo) => _context.Remove(todo);
    }
}