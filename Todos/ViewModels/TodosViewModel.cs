using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Todos.Commands;
using Todos.Extensions;
using Todos.Models;
using Todos.Services;

namespace Todos.ViewModels
{
    public class TodosViewModel : ViewModelBase
    {
        private readonly AddTodoViewModel _addTodoViewModel;
        private readonly EditTodoViewModel _editTodoViewModel;
        private readonly ITodoService _todoService;
        private ObservableCollection<Todo> _todos;
        private ObservableCollection<Todo> _processedTodos;
        private ViewModelClosable _current;
        private Todo _selected;
        private string _search = string.Empty;

        public Command AddCommand { get; }
        public Command EditCommand { get; }
        public Command RemoveCommand { get; }
        public Command RefreshCommand { get; }

        public ViewModelClosable Current
        {
            get => _current;
            set => SetProperty(ref _current, value);
        }

        public ObservableCollection<Todo> Todos
        {
            get => _todos;
            set => SetProperty(ref _todos, value);
        }

        public ObservableCollection<Todo> ProcessedTodos
        {
            get => _processedTodos;
            set => SetProperty(ref _processedTodos, value);
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

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                OnSearchChanged(_search);
            }
        }

        public TodosViewModel(ITodoService todoService, AddTodoViewModel addTodoViewModel,
            EditTodoViewModel editTodoViewModel)
        {
            _todoService = todoService;
            _addTodoViewModel = addTodoViewModel;
            _editTodoViewModel = editTodoViewModel;

            AddCommand = new Command(Add);
            EditCommand = new Command(Edit, CanEdit);
            RemoveCommand = new Command(Remove, CanRemove);
            RefreshCommand = new Command(Refresh);

            _addTodoViewModel.Close += OnClose;
            _editTodoViewModel.Close += OnClose;
            SelectionChanged += OnCanEditChanged;
            SelectionChanged += OnCanRemoveChanged;
            SearchChanged += ProcessTodos;

            LoadDataAsync();
        }

        public event PropertyChangedEventHandler SelectionChanged;
        public event SearchChangedEventHandler SearchChanged;

        public void OnSearchChanged(string search) => SearchChanged?.Invoke(this, new SearchChangedEventArgs(search));

        public void OnSelectionChanged([CallerMemberName] string propertyName = null) =>
            SelectionChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected)));

        // Todo: Maybe add a DataChanged property that will be called after Todos changes?
        public async void LoadDataAsync()
        {
            Todos = new ObservableCollection<Todo>(await _todoService.GetAsync());
            ProcessTodos(this, new SearchChangedEventArgs(Search));
        }

        private void Add() => Current = _addTodoViewModel;

        private void Edit()
        {
            Selected.Copy(_editTodoViewModel);
            Current = _editTodoViewModel;
        }

        private bool CanEdit() => Selected != null;

        private void Remove()
        {
            _todoService.Remove(Selected);
            Todos.Remove(Selected);
            ProcessedTodos.Remove(Selected);
        }

        private bool CanRemove() => Selected != null;

        private void Refresh() => LoadDataAsync();

        private void ProcessTodos(object sender, SearchChangedEventArgs e) => ProcessedTodos =
            new ObservableCollection<Todo>(_todos.Where(x => x.Title.Contains(e.Search)));

        private void OnClose(object sender, EventArgs e) => Current = null;

        private void OnCanEditChanged(object sender, EventArgs e) => EditCommand.OnCanExecuteChanged();

        private void OnCanRemoveChanged(object sender, EventArgs e) => RemoveCommand.OnCanExecuteChanged();
    }
}