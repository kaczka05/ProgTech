using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Models;
using LibraryLogicLayer;

namespace WpfApp1.ViewModels
{
    public class CatalogViewModel : INotifyPropertyChanged
    {
        private readonly ILibraryDataService _libraryService;
        private CatalogModel? _selectedCatalog;

        public ObservableCollection<CatalogModel> Catalogs { get; } = new();

        public CatalogModel? SelectedCatalog
        {
            get => _selectedCatalog;
            set
            {
                _selectedCatalog = value;
                OnPropertyChanged(nameof(SelectedCatalog));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand AddCatalogCommand { get; }
        public ICommand DeleteCatalogCommand { get; }

        public CatalogViewModel()
        {   

            _libraryService = ILibraryDataService.CreateNewService();
            AddCatalogCommand = new RelayCommand(async _ => await AddCatalogAsync());
            DeleteCatalogCommand = new RelayCommand(async _ => await DeleteCatalogAsync(), _ => SelectedCatalog != null);
            _ = LoadCatalogsAsync();
        }

        private async Task LoadCatalogsAsync()
        {
            /*var logicCatalogs = await Task.Run(() => _libraryService.GetAllCatalogsAsync());
            Catalogs.Clear();
            foreach (var c in logicCatalogs)
            {
                Catalogs.Add(new CatalogModel
                {
                    CatalogId = c.CatalogId,
                    Title = c.Title,
                    Author = c.Author,
                    NrOfPages = c.NrOfPages
                });
            }*/
        }

        private async Task AddCatalogAsync()
        {
            int newId = Catalogs.Any() ? Catalogs.Max(x => x.CatalogId) + 1 : 1;
            await _libraryService.AddCatalogAsync(newId, "New Author", "New Book", 100);
            await LoadCatalogsAsync();
        }

        private async Task DeleteCatalogAsync()
        {
            if (SelectedCatalog != null)
            {
                await _libraryService.RemoveCatalogAsync(SelectedCatalog.CatalogId);
                await LoadCatalogsAsync();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
