using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Todos.Models;
using Todos.Services;

namespace Todos.ViewModels
{
    public class TodosViewModel : ViewModelBase
    {
        private readonly ITodoService _todoService;
        private ObservableCollection<Todo> _todos;
        private Todo _selected;
        
        public ObservableCollection<Todo> Todos
        {
            get => _todos;
            set => SetProperty(ref _todos, value);
        }

        public Todo Selected
        {
            get => _selected;
            set
            {
                SetProperty(ref _selected, value);
                OnSelectionChanged(nameof(Selected));
            }
        }

        public TodosViewModel(ITodoService todoService)
        {
            _todoService = todoService;

            LoadDataAsync();
        }

        public void RemoveSelected()
        {
            _todoService.Remove(Selected);
            Todos.Remove(Selected);
        }

        public event PropertyChangedEventHandler SelectionChanged;

        public void OnSelectionChanged([CallerMemberName] string propertyName = null) =>
            SelectionChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected)));

        public async void LoadDataAsync() => Todos = new ObservableCollection<Todo>(await _todoService.GetAsync());
    }
}