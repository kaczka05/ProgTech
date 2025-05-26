using LibraryLogicLayer;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfApp1.ViewModels;

namespace TestWpfApp1
{
    // Define the same interface used by the logic layer
    

    // Fake object implementing ILogicCatalog
    public class FakeCatalog : ILogicCatalog
    {


        public int CatalogId { get; init; }
        public string Title { get; init; } = "";
        public string Author { get; init; } = "";
        public int NrOfPages { get; init; }

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

        

            public Task AddCatalogAsync(int id, string author, string title, int nrOfPages)
            {
                _fake.CatalogsToReturn.Add(new FakeCatalog
                {
                    CatalogId = id,
                    Author = author,
                    Title = title,
                    NrOfPages = nrOfPages
                });
                return Task.CompletedTask;
            }

            public Task RemoveCatalogAsync(int id) 
            { 
                _fake.CatalogsToReturn.RemoveAll(c => c.CatalogId == id);
                return Task.CompletedTask;
            }

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

            public List<LibraryLogicLayer.ILogicCatalog> GetAllCatalogsAsync()
            {
                return _fake.GetAllCatalogs().Cast<LibraryLogicLayer.ILogicCatalog>().ToList();
            }

            public void LogicAddCatalogue(int v1, string v2, string v3, int v4)
            {
                throw new NotImplementedException();
            }

            public void LogicRemoveCatalogue(int v)
            {
                throw new NotImplementedException();
            }

            public void LogicAddUser(int v1, string v2, string v3)
            {
                throw new NotImplementedException();
            }

            public void LogicRemoveUser(int v)
            {
                throw new NotImplementedException();
            }

            public void LogicAddUserEvent(int v1, int v2, int v3, int v4, bool v5)
            {
                throw new NotImplementedException();
            }
        }

    }




    [TestClass]
    public class CatalogViewModelTests
    {
        public static class StaticTestData
        {
            public static (int Id, string Title, string Author, int Pages) GetTestData()
            {
                return (101, "StaticTitle", "StaticAuthor", 123);
            }
        }

        public static class RandomTestData
        {
            private static readonly System.Random rnd = new();

            public static (int Id, string Title, string Author, int Pages) GetTestData()
            {
                int id = rnd.Next(1000, 9999);
                string title = "Title" + rnd.Next(1, 100);
                string author = "Author" + rnd.Next(1, 100);
                int pages = rnd.Next(50, 500);
                return (id, title, author, pages);
            }
        }
        private async Task WaitForConditionAsync(Func<bool> condition, int timeoutMs = 2000, int pollMs = 50)
        {
            var start = DateTime.UtcNow;
            while (!condition())
            {
                if ((DateTime.UtcNow - start).TotalMilliseconds > timeoutMs)
                    break;
                await Task.Delay(pollMs);
            }
        }
        
        [TestMethod]
        public async Task AddCatalogCommand_AddsNewCatalog()
        {
    
            var fakeService = new FakeLibraryDataService();
            fakeService.CatalogsToReturn.Add(new FakeCatalog
            {
                CatalogId = 1,
                Title = "New Book",
                Author = "New Author",
                NrOfPages = 100
            });
            Assert.AreEqual(1, fakeService.CatalogsToReturn.Count);
            Assert.AreEqual("New Book", fakeService.CatalogsToReturn[0].Title);
            Assert.AreEqual("New Author", fakeService.CatalogsToReturn[0].Author);
            Assert.AreEqual(100, fakeService.CatalogsToReturn[0].NrOfPages);
            var viewModel = new TestableCatalogViewModel(fakeService);

        
            await Task.Delay(200);
            Assert.IsTrue(viewModel.AddCatalogCommand.CanExecute(null));
            await Task.Run(() => viewModel.AddCatalogCommand.Execute(null));
            await WaitForConditionAsync(() => viewModel.Catalogs.Any(b => b.Title == "New Book"));
            Assert.IsTrue(viewModel.Catalogs.Any(b => b.Title == "New Book"));

            


        }
        
     
    }
}
