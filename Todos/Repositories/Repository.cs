using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Todos.Repositories
{
    public abstract class Repository<T> : IRepository<T>
    {
        private readonly string _endpointUrl;

        protected Repository(string endpointUrl) => _endpointUrl = endpointUrl;

        public async Task<T> GetAsync()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_endpointUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException();
            }

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}