namespace Todos.Services
{
    public class SettingsService : ISettingsService
    {
        public string TodosEndpointUrl => Properties.Settings.Default.TodosEndpointUrl;
    }
}
