using System;

namespace Todos
{
    public delegate void SearchChangedEventHandler(object sender, SearchChangedEventArgs e);

    public class SearchChangedEventArgs : EventArgs
    {
        public string Search { get; }

        public SearchChangedEventArgs(string search) => Search = search;
    }
}
