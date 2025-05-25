using System.ComponentModel;

namespace LibraryApp.Models
{//
    public class CatalogModel : INotifyPropertyChanged
    {
        private int _catalogId;
        private string _title;
        private string _author;
        private int _nrOfPages;

        public int CatalogId
        {
            get => _catalogId;
            set { _catalogId = value; OnPropertyChanged(nameof(CatalogId)); }
        }

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        public string Author
        {
            get => _author;
            set { _author = value; OnPropertyChanged(nameof(Author)); }
        }

        public int NrOfPages
        {
            get => _nrOfPages;
            set { _nrOfPages = value; OnPropertyChanged(nameof(NrOfPages)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
