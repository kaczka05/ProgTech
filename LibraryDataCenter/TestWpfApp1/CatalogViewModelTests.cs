using LibraryLogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace TestWpfApp1
{
    // Fake that mimics the logic layer interface (WITHOUT referencing it)
    public class FakeLibraryDataService
    {
        public List<CatalogModel> CatalogsToReturn = new();

        public IEnumerable<CatalogModel> GetAllCatalogs()
        {
            return CatalogsToReturn;
        }
    }

    // Helper ViewModel that accepts our fake directly
    public class TestableCatalogViewModel : CatalogViewModel
    {
        public TestableCatalogViewModel(FakeLibraryDataService fake)
            : base(new Adapter(fake)) { }

        // Adapter that adapts fake to expected logic interface
        private class Adapter : LibraryLogicLayer.ILibraryDataService
        {
            private readonly FakeLibraryDataService _fake;

            public Adapter(FakeLibraryDataService fake)
            {
                _fake = fake;
            }

            public Task AddCatalogAsync(int id, string author, string title, int nrOfPages)
            {
                throw new NotImplementedException();
            }

            public Task AddDatabaseEventAsync(int id, int employeeId, int stateId, bool addition)
            {
                throw new NotImplementedException();
            }

            public Task AddStateAsync(int id, int nrOfBooks, int catalogId)
            {
                throw new NotImplementedException();
            }

            public Task AddUserAsync(int id, string firstName, string lastName)
            {
                throw new NotImplementedException();
            }

            public Task AddUserEventAsync(int id, int employeeId, int stateId, int userId, bool borrowing)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<CatalogModel> GetAllCatalogs()
            {
                return _fake.GetAllCatalogs();
            }

            public List<ILogicCatalog> GetAllCatalogsAsync()
            {
                throw new NotImplementedException();
            }

            public List<ILogicEvent> GetAllEventsAsync()
            {
                throw new NotImplementedException();
            }

            public List<ILogicState> GetAllStatesAsync()
            {
                throw new NotImplementedException();
            }

            public List<ILogicUser> GetAllUsersAsync()
            {
                throw new NotImplementedException();
            }

            public Task RemoveCatalogAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task RemoveEventAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task RemoveStateAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task RemoveUserAsync(int id)
            {
                throw new NotImplementedException();
            }
        }
    }

    [TestClass]
    public class CatalogViewModelTests
    {
        [TestMethod]
        public void ViewModel_LoadsCatalogsFromFakeService()
        {
            // Arrange
            var fake = new FakeLibraryDataService();
            fake.CatalogsToReturn.Add(new CatalogModel
            {

                Title = "Test Book",
                Author = "Tester",

            });

            var vm = new TestableCatalogViewModel(fake);

            // Act
            var catalogs = vm.Catalogs;

            // Assert
            Assert.AreEqual(1, catalogs.Count);
            Assert.AreEqual("Test Book", catalogs[0].Title);
        }
    }
}
