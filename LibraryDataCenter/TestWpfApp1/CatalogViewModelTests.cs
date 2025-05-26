using LibraryLogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfApp1.ViewModels;

namespace TestWpfApp1
{
    public interface ILogicCatalog
    {
        int CatalogId { get; }
        string Title { get; }
        string Author { get; }
        int NrOfPages { get; }
    }

    public class FakeCatalog : ILogicCatalog
    {
        public int CatalogId { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public int NrOfPages { get; set; }
    }

    public class FakeLibraryDataService
    {
        public List<ILogicCatalog> CatalogsToReturn = new();

        public IEnumerable<ILogicCatalog> GetAllCatalogs()
        {
            return CatalogsToReturn;
        }
    }

    public class TestableCatalogViewModel : CatalogViewModel
    {
        public TestableCatalogViewModel(FakeLibraryDataService fake)
            : base(new Adapter(fake)) { }

        private class Adapter : LibraryLogicLayer.ILibraryDataService
        {
            private readonly FakeLibraryDataService _fake;

            public Adapter(FakeLibraryDataService fake)
            {
                _fake = fake;
            }

            public Task<List<ILogicCatalog>> GetAllCatalogsAsync()
            {
                return Task.FromResult(_fake.GetAllCatalogs().ToList());
            }

            public Task AddCatalogAsync(int id, string author, string title, int nrOfPages) => Task.CompletedTask;
            public Task RemoveCatalogAsync(int id) => Task.CompletedTask;

            // Other interface methods not used here can throw
            public Task AddDatabaseEventAsync(int id, int employeeId, int stateId, bool addition) => throw new NotImplementedException();
            public Task AddStateAsync(int id, int nrOfBooks, int catalogId) => throw new NotImplementedException();
            public Task AddUserAsync(int id, string firstName, string lastName) => throw new NotImplementedException();
            public Task AddUserEventAsync(int id, int employeeId, int stateId, int userId, bool borrowing) => throw new NotImplementedException();
            public List<ILogicEvent> GetAllEventsAsync() => throw new NotImplementedException();
            public List<ILogicState> GetAllStatesAsync() => throw new NotImplementedException();
            public List<ILogicUser> GetAllUsersAsync() => throw new NotImplementedException();
            public Task RemoveEventAsync(int id) => throw new NotImplementedException();
            public Task RemoveStateAsync(int id) => throw new NotImplementedException();
            public Task RemoveUserAsync(int id) => throw new NotImplementedException();

            List<LibraryLogicLayer.ILogicCatalog> ILibraryDataService.GetAllCatalogsAsync()
            {
                throw new NotImplementedException();
            }
        }
    }

    [TestClass]
    public class CatalogViewModelTests
    {
        [TestMethod]
        public async Task ViewModel_LoadsCatalogsFromFakeService()
        {
            // Arrange
            var fake = new FakeLibraryDataService();
            fake.CatalogsToReturn.Add(new FakeCatalog
            {
                CatalogId = 1,
                Title = "Test Book",
                Author = "Tester",
                NrOfPages = 123
            });

            var vm = new TestableCatalogViewModel(fake);

            // Wait for async load
            await Task.Delay(100); // Optional wait to ensure async runs

            // Act
            var catalogs = vm.Catalogs;

            // Assert
            Assert.AreEqual(1, catalogs.Count);
            Assert.AreEqual("Test Book", catalogs[0].Title);
        }
    }
}
