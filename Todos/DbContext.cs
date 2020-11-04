using System.Collections.Generic;
using System.Linq;
using Todos.Extensions;
using Todos.Models;

namespace Todos
{
    public class DbContext
    {
        private readonly List<Todo> _todos;

        public IEnumerable<Todo> Todos => new List<Todo>(_todos);

        public DbContext() =>
            _todos = new List<Todo>
            {
                new Todo
                {
                    Id = 0,
                    Title = "Купить хлеб",
                    Completed = false
                },
                new Todo
                {
                    Id = 1,
                    Title = "Купить масло",
                    Completed = false
                },
                new Todo
                {
                    Id = 2,
                    Title = "Купить пиво",
                    Completed = true
                }
            };

        public void Add(Todo todo)
        {
            todo.Id = GenerateId();

            _todos.Add(todo);
        }

        public void Update(Todo todo)
        {
            var entity = _todos.Find(x => x.Id == todo.Id);

            if (entity == null)
            {
                throw new KeyNotFoundException();
            }

            todo.Copy(entity);
        }

        public void Remove(Todo todo)
        {
            var entity = _todos.Find(x => x.Id == todo.Id);

            if (entity == null)
            {
                throw new KeyNotFoundException();
            }

            _todos.Remove(entity);
        }

        private int GenerateId() => _todos.Count > 0 ? _todos.Max(x => x.Id) + 1 : 0;
    }
}