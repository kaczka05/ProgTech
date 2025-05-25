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
    using System.Reflection;

    namespace LibraryDataLayer.Tests
    {
        [TestClass]
        public class LibraryDataRepositoryIntegrationTests
        {
            private ILibraryDataRepository repo;

            [TestInitialize]
            public void Setup()
            {
                // U¿yj connection stringa do bazy testowej lub in-memory
                string testConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryTestDb;Integrated Security=True;";
                repo = ILibraryDataRepository.CreateNewRepository(testConnectionString);

                // Wyczyœæ dane testowe
                foreach (var c in repo.GetAllCatalogs().ToList())
                    repo.RemoveCatalogById(c.CatalogId);
            }

            [TestMethod]
            public void AddCatalog_And_GetCatalogById_Works()
            {
                repo.AddCatalog(1, "Title", "Author", 123);
                var catalog = repo.GetCatalogById(1);

                Assert.IsNotNull(catalog);
                Assert.AreEqual("Title", catalog.Title);
                Assert.AreEqual("Autor", catalog.Author);
                Assert.AreEqual(123, catalog.NrOfPages);
            }

            [TestMethod]
            public void RemoveCatalogById_RemovesCatalog()
            {
                repo.AddCatalog(2, "Tytul2", "Autor2", 222);
                repo.RemoveCatalogById(2);
                var catalog = repo.GetCatalogById(2);

                Assert.IsNull(catalog);
            }
        
        

            [TestMethod]
            public void AddCatalog_And_GetCatalogById_RandomData()
            {
                var data = RandomTestData.GetTestData();
                repo.AddCatalog(data.Id, data.Title, data.Author, data.Pages);

                var catalog = repo.GetCatalogById(data.Id);

                Assert.IsNotNull(catalog);
                Assert.AreEqual(data.Title, catalog.Title);
                Assert.AreEqual(data.Author, catalog.Author);
                Assert.AreEqual(data.Pages, catalog.NrOfPages);
            }

        }

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
    }



}
