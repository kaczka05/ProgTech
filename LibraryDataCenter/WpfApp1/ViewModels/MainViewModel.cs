using WpfApp1.Models;
using WpfApp1.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CatalogModel> Catalogs { get; set; } = new();
        private CatalogModel? _selectedCatalog;

        public CatalogModel? SelectedCatalog
        {
            get => _selectedCatalog;
            set { _selectedCatalog = value; OnPropertyChanged(nameof(SelectedCatalog)); }
        }

        public ICommand AddCatalogCommand { get; }
        public ICommand DeleteCatalogCommand { get; }

        public MainViewModel()
        {
            // Dummy data to test
            Catalogs.Add(new CatalogModel { CatalogId = 1, Title = "Book 1", Author = "Author A", NrOfPages = 200 });
            Catalogs.Add(new CatalogModel { CatalogId = 2, Title = "Book 2", Author = "Author B", NrOfPages = 300 });

            AddCatalogCommand = new RelayCommand(_ => AddCatalog());
            DeleteCatalogCommand = new RelayCommand(_ => DeleteCatalog(), _ => SelectedCatalog != null);
        }

        private void AddCatalog()
        {
            Catalogs.Add(new CatalogModel { CatalogId = Catalogs.Count + 1, Title = "New Book", Author = "New Author", NrOfPages = 100 });
        }

        private void DeleteCatalog()
        {
            if (SelectedCatalog != null)
                Catalogs.Remove(SelectedCatalog);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
