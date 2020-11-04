namespace Todos.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _todosViewModel;

        public ViewModelBase Current
        {
            get => _todosViewModel;
            set => SetProperty(ref _todosViewModel, value);
        }

        public MainWindowViewModel(TodosViewModel todosViewModel) => _todosViewModel = todosViewModel;
    }
}