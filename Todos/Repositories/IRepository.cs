using System.Threading.Tasks;

namespace Todos.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetAsync();
    }
}
