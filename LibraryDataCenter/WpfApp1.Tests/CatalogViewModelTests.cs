using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp1.ViewModels;
using WpfApp1.Models;
using WpfApp1.Tests.Fakes;
using System.Linq;

namespace WpfApp1.Tests
{
    [TestClass]
    public class CatalogViewModelTests
    {
        private CatalogViewModel viewModel;
        private FakeLibraryDataService fakeService;

        [TestInitialize]
        public void Setup()
        {
            fakeService = new FakeLibraryDataService();
            viewModel = new CatalogViewModel(fakeService);
        }

        [TestMethod]
        public void Catalogs_ShouldBeLoaded_FromFakeService()
        {
            Assert.AreEqual(2, viewModel.Catalogs.Count);
        }

        [TestMethod]
        public void AddCommand_ShouldAddCatalog_ToServiceAndViewModel()
        {
            viewModel.NewCatalogId = 10;
            viewModel.NewCatalogTitle = "Test Title";
            viewModel.NewCatalogAuthor = "Test Author";
            viewModel.NewCatalogPages = 123;

            viewModel.AddCommand.Execute(null);

            Assert.AreEqual(3, viewModel.Catalogs.Count);
            Assert.IsTrue(fakeService.AddedCatalogs.Any(c => c.Id == 10));
        }

        [TestMethod]
        public void RemoveCommand_ShouldRemoveCatalog_ById()
        {
            var toRemove = viewModel.Catalogs.First();
            viewModel.SelectedCatalog = toRemove;

            viewModel.RemoveCommand.Execute(null);

            Assert.AreEqual(1, viewModel.Catalogs.Count);
            Assert.IsTrue(fakeService.RemovedCatalogs.Contains(toRemove.Id));
        }
    }
}
