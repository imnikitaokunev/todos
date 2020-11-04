using System;

namespace Todos.ViewModels
{
    public abstract class ViewModelClosable: ViewModelBase
    {
        public event EventHandler Close;

        protected virtual void OnClose()
        {
            Close?.Invoke(this, null);
        }
    }
}
