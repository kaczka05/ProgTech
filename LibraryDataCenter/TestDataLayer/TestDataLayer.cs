using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using LibraryDataLayer;
using System.Data.Linq;

namespace TestDataLayer
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    namespace LibraryDataLayer.Tests
    {
        [TestClass]
        public class LibraryDataRepositoryIntegrationTests
        {
            private ILibraryDataRepository _repo;

            [TestInitialize]
            public void Setup()
            {
                // U¿yj connection stringa do bazy testowej lub in-memory
                string testConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryTestDb;Integrated Security=True;";
                _repo = ILibraryDataRepository.CreateNewRepository(testConnectionString);

                // Wyczyœæ dane testowe
                foreach (var c in _repo.GetAllCatalogs().ToList())
                    _repo.RemoveCatalogById(c.CatalogId);
            }

            [TestMethod]
            public void AddCatalog_And_GetCatalogById_Works()
            {
                _repo.AddCatalog(1, "Title", "Author", 123);
                var catalog = _repo.GetCatalogById(1);

                Assert.IsNotNull(catalog);
                Assert.AreEqual("Title", catalog.Title);
                Assert.AreEqual("Autor", catalog.Author);
                Assert.AreEqual(123, catalog.NrOfPages);
            }

            [TestMethod]
            public void RemoveCatalogById_RemovesCatalog()
            {
                _repo.AddCatalog(2, "Tytul2", "Autor2", 222);
                _repo.RemoveCatalogById(2);
                var catalog = _repo.GetCatalogById(2);

                Assert.IsNull(catalog);
            }
        }
    }

}