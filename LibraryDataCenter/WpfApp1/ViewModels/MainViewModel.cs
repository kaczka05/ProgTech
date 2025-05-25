using LibraryLogicLayer;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ILibraryDataService _dataService;

        public ObservableCollection<ILogicCatalog> Catalogs { get; set; }

        public MainViewModel(ILibraryDataService dataService)
        {
            _dataService = dataService;
            LoadCatalogs();
        }

        private void LoadCatalogs()
        {
            var catalogs = _dataService.GetAllCatalogsAsync(); // Sync version from your service
            Catalogs = new ObservableCollection<ILogicCatalog>(catalogs);
            OnPropertyChanged(nameof(Catalogs));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
