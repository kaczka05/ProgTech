
using System.Collections.ObjectModel;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1.Tests.Fakes
{
    public class FakeLibraryDataService : ILibraryDataService
    {
        public ObservableCollection<CatalogModel> Catalogs { get; set; } = new ObservableCollection<CatalogModel>();
        public List<CatalogModel> AddedCatalogs { get; } = new();
        public List<int> RemovedCatalogs { get; } = new();

        public FakeLibraryDataService()
        {
            Catalogs.Add(new CatalogModel { Id = 1, Title = "Title 1", Author = "Author A", NumberOfPages = 100 });
            Catalogs.Add(new CatalogModel { Id = 2, Title = "Title 2", Author = "Author B", NumberOfPages = 200 });
        }

        public ObservableCollection<CatalogModel> GetCatalogs()
        {
            return Catalogs;
        }

        public void AddCatalog(int id, string title, string author, int pages)
        {
            var model = new CatalogModel { Id = id, Title = title, Author = author, NumberOfPages = pages };
            Catalogs.Add(model);
            AddedCatalogs.Add(model);
        }

        public void RemoveCatalogById(int id)
        {
            var item = Catalogs.FirstOrDefault(c => c.Id == id);
            if (item != null)
                Catalogs.Remove(item);

            RemovedCatalogs.Add(id);
        }
    }
}
