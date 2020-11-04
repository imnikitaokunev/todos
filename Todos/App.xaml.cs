using System.Windows;
using Autofac;
using Todos.Repositories;
using Todos.Services;
using Todos.ViewModels;

namespace Todos
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DbContext>().AsSelf().SingleInstance();
            builder.RegisterType<TodoRepository>().As<ITodoRepository>();
            builder.RegisterType<TodoService>().As<ITodoService>();

            builder.RegisterType<TodosViewModel>().AsSelf();
            builder.RegisterType<AddTodoViewModel>().AsSelf();
            builder.RegisterType<EditTodoViewModel>().AsSelf();
            builder.RegisterType<MainWindowViewModel>().AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var window = scope.Resolve<MainWindow>();
                window.DataContext = container.Resolve<MainWindowViewModel>();
                window.Show();
            }
        }
    }
}
