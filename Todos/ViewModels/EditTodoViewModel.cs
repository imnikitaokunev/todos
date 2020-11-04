using Mapster;
using Todos.Commands;
using Todos.Models;
using Todos.Services;

namespace Todos.ViewModels
{
    public class EditTodoViewModel : ViewModelClosable
    {
        private readonly ITodoService _todoService;
        private int _id;
        private string _title;
        private bool _completed;

        public Command SaveCommand { get; }
        public Command CloseCommand { get; }

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public bool Completed
        {
            get => _completed;
            set => SetProperty(ref _completed, value);
        }

        public EditTodoViewModel(ITodoService todoService)
        {
            _todoService = todoService;

            SaveCommand = new Command(Save, CanSave);
            CloseCommand = new Command(OnClose);
        }

        private void Save()
        {
            var todo = this.Adapt<Todo>();
            _todoService.Update(todo);
        }

        private bool CanSave() => true;
    }
}
