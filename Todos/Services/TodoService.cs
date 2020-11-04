using System.Collections.Generic;
using System.Threading.Tasks;
using Todos.Models;
using Todos.Repositories;

namespace Todos.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository) => _todoRepository = todoRepository;

        public async Task<IEnumerable<Todo>> GetAsync() => await _todoRepository.GetAsync();

        public void Add(Todo todo) => _todoRepository.Add(todo);

        public void Update(Todo todo) => _todoRepository.Update(todo);
        public void Remove(Todo todo) => _todoRepository.Remove(todo);
    }
}