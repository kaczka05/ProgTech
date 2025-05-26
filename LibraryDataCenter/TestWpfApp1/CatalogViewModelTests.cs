using LibraryLogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfApp1.ViewModels;

namespace TestWpfApp1
{
    // Define the same interface used by the logic layer
    public interface ILogicCatalog
    {
        int CatalogId { get; }
        string Title { get; }
        string Author { get; }
        int NrOfPages { get; }
    }

    // Fake object implementing ILogicCatalog
    public class FakeCatalog : ILogicCatalog
    {
        public int CatalogId { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public int NrOfPages { get; set; }
    }

    // Fake service that returns fake catalogs
    public class FakeLibraryDataService
    {
        public List<ILogicCatalog> CatalogsToReturn = new();

        public IEnumerable<ILogicCatalog> GetAllCatalogs()
        {
            return CatalogsToReturn;
        }
    }

    // ViewModel that uses the fake service via adapter
    public class TestableCatalogViewModel : CatalogViewModel
    {
        public TestableCatalogViewModel(FakeLibraryDataService fake)
            : base(new Adapter(fake)) { }

        // Adapter translates fake to real service interface
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

            // Not needed in this test
            public Task AddDatabaseEventAsync(int id, int employeeId, int stateId, bool addition) => throw new System.NotImplementedException();
            public Task AddStateAsync(int id, int nrOfBooks, int catalogId) => throw new System.NotImplementedException();
            public Task AddUserAsync(int id, string firstName, string lastName) => throw new System.NotImplementedException();
            public Task AddUserEventAsync(int id, int employeeId, int stateId, int userId, bool borrowing) => throw new System.NotImplementedException();
            public List<ILogicEvent> GetAllEventsAsync() => throw new System.NotImplementedException();
            public List<ILogicState> GetAllStatesAsync() => throw new System.NotImplementedException();
            public List<ILogicUser> GetAllUsersAsync() => throw new System.NotImplementedException();
            public Task RemoveEventAsync(int id) => throw new System.NotImplementedException();
            public Task RemoveStateAsync(int id) => throw new System.NotImplementedException();
            public Task RemoveUserAsync(int id) => throw new System.NotImplementedException();

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
            // Arrange: create fake service with one catalog
            var fake = new FakeLibraryDataService();
            fake.CatalogsToReturn.Add(new FakeCatalog
            {
                CatalogId = 1,
                Title = "Test Book",
                Author = "Tester",
                NrOfPages = 123
            });

            var vm = new TestableCatalogViewModel(fake);

            // Act: wait for async data loading
            await Task.Delay(200); // Give time for LoadCatalogsAsync in constructor

            // Assert: verify data was loaded
            Assert.AreEqual("Test Book", vm.Catalogs[0].Title);
            Assert.AreEqual(1, vm.Catalogs.Count);
            Assert.AreEqual("Test Book", vm.Catalogs[0].Title);
        }
    }
}
