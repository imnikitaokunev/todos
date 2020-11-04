using System;
using System.Linq;
using Todos.Commands;
using Todos.Extensions;

namespace Todos.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly TodosViewModel _todosViewModel;
        private readonly AddTodoViewModel _addTodoViewModel;
        private EditTodoViewModel _editTodoViewModel;
        private ViewModelClosable _secondary;
        private ViewModelBase _main;

        public Command AddCommand { get; }
        public Command EditCommand { get; }
        public Command RemoveCommand { get; }
        public Command RefreshCommand { get; }

        public ViewModelClosable Secondary
        {
            get => _secondary;
            set => SetProperty(ref _secondary, value);
        }

        public ViewModelBase Main
        {
            get => _main;
            set => SetProperty(ref _main, value);
        }

        public MainWindowViewModel(TodosViewModel todosViewModel, AddTodoViewModel addTodoViewModel,
            EditTodoViewModel editTodoViewModel)
        {
            _todosViewModel = todosViewModel;
            _addTodoViewModel = addTodoViewModel;
            _editTodoViewModel = editTodoViewModel;

            Main = _todosViewModel;

            AddCommand = new Command(Add);
            EditCommand = new Command(Edit, CanEdit);
            RemoveCommand = new Command(Remove, CanRemove);
            RefreshCommand = new Command(Refresh);

            _addTodoViewModel.Close += OnClose;
            _editTodoViewModel.Close += OnClose;
            _todosViewModel.SelectionChanged += OnCanEditChanged;
            _todosViewModel.SelectionChanged += OnCanRemoveChanged;
        }

        private void Add() => Secondary = _addTodoViewModel;

        private void Edit()
        {
            _todosViewModel.Selected.Copy(_editTodoViewModel);
            Secondary = _editTodoViewModel;
        }

        private bool CanEdit() => _todosViewModel.Selected != null;

        private void Remove() => _todosViewModel.RemoveSelected();

        private bool CanRemove() => _todosViewModel.Selected != null;

        private void Refresh() => _todosViewModel.LoadDataAsync();

        private void OnClose(object sender, EventArgs e) => Secondary = null;

        private void OnCanEditChanged(object sender, EventArgs e) => EditCommand.OnCanExecuteChanged();

        private void OnCanRemoveChanged(object sender, EventArgs e) => RemoveCommand.OnCanExecuteChanged();

    }
}