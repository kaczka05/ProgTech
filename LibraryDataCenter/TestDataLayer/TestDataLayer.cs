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
                string testConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=D:\\VIsualStudioProjects\\ProgTech\\LibraryDataCenter\\LibraryDataCenter\\Database1.mdf;Integrated Security=True;";
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
                Assert.AreEqual("Author", catalog.Author);
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

        [TestClass]
        public class LibraryDataRepositoryTests
        {
            private ILibraryDataRepository _repo;

            [TestInitialize]
            public void Setup()
            {
                _repo = new LibraryDataRepository(); // Offline mode
            }

            [TestMethod]
            public void AddAndGetCatalog_Works()
            {
                _repo.AddCatalog(1, "Test Book", "Author A", 100);
                var result = _repo.GetCatalogById(1);
                Assert.IsNotNull(result);
            }

            [TestMethod]
            public void RemoveCatalogById_Works()
            {
                _repo.AddCatalog(2, "To Delete", "Author B", 150);
                _repo.RemoveCatalogById(2);
                var result = _repo.GetCatalogById(2);
                Assert.IsNull(result);
            }

            [TestMethod]
            public void GetAllCatalogs_ReturnsItems()
            {
                _repo.AddCatalog(3, "Catalog A", "Author X", 200);
                var all = _repo.GetAllCatalogs();
                Assert.IsTrue(all.Any(c => c.CatalogId == 3));
            }

            [TestMethod]
            public void AddAndGetUser_Works()
            {
                _repo.AddUser(1, "John", "Doe");
                var user = _repo.GetUserById(1);
                Assert.IsNotNull(user);
            }

            [TestMethod]
            public void RemoveUser_Works()
            {
                _repo.AddUser(2, "Jane", "Smith");
                _repo.RemoveUserById(2);
                var user = _repo.GetUserById(2);
                Assert.IsNull(user);
            }

            [TestMethod]
            public void GetAllUsers_Works()
            {
                _repo.AddUser(3, "Jack", "Ryan");
                var users = _repo.GetAllUsers();
                Assert.IsTrue(users.Any(u => u.UserId == 3));
            }

            [TestMethod]
            public void AddAndGetState_Works()
            {
                _repo.AddCatalog(10, "State Book", "Author S", 123);
                _repo.AddState(1, 5, 10);
                var state = _repo.GetStateById(1);
                Assert.IsNotNull(state);
            }

            [TestMethod]
            public void RemoveState_Works()
            {
                _repo.AddCatalog(11, "Another Book", "Author Z", 321);
                _repo.AddState(2, 3, 11);
                _repo.RemoveStateByID(2);
                var state = _repo.GetStateById(2);
                Assert.IsNull(state);
            }

            [TestMethod]
            public void GetAllStates_Works()
            {
                _repo.AddCatalog(12, "GetAll Book", "Author Y", 111);
                _repo.AddState(3, 10, 12);
                var states = _repo.GetAllStates();
                Assert.IsTrue(states.Any(s => s.StateId == 3));
            }

            [TestMethod]
            public void AddAndGetDatabaseEvent_Works()
            {
                _repo.AddUser(10, "Emp", "Loyee");
                _repo.AddCatalog(20, "Event Book", "Ev Author", 222);
                _repo.AddState(10, 2, 20);

                _repo.AddDatabaseEvent(1, 10, 10, true);
                var ev = _repo.GetEventById(1);
                Assert.IsNotNull(ev);
            }

            [TestMethod]
            public void AddAndGetUserEvent_Works()
            {
                _repo.AddUser(11, "Emp", "One");
                _repo.AddUser(12, "Usr", "Two");
                _repo.AddCatalog(21, "Usr Book", "Usr Auth", 333);
                _repo.AddState(11, 1, 21);

                _repo.AddUserEvent(2, 11, 11, 12, true);
                var ev = _repo.GetEventById(2);
                Assert.IsNotNull(ev);
            }

            [TestMethod]
            public void RemoveEvent_Works()
            {
                _repo.AddUser(13, "Emp", "Del");
                _repo.AddCatalog(22, "Del Book", "Del Auth", 444);
                _repo.AddState(12, 2, 22);
                _repo.AddDatabaseEvent(3, 13, 12, false);
                _repo.RemoveEventById(3);
                var ev = _repo.GetEventById(3);
                Assert.IsNull(ev);
            }

            [TestMethod]
            public void GetAllEvents_Works()
            {
                _repo.AddUser(14, "Event", "Checker");
                _repo.AddCatalog(23, "EventCat", "Checker", 101);
                _repo.AddState(13, 4, 23);
                _repo.AddDatabaseEvent(4, 14, 13, true);
                var allEvents = _repo.GetAllEvents();
                Assert.IsTrue(allEvents.Any(e => e.EventId == 4));
            }

            [TestMethod]
            public void ExistenceChecks_Work()
            {
                _repo.AddCatalog(30, "Exist Book", "Auth E", 100);
                _repo.AddUser(20, "Exist", "User");
                _repo.AddState(20, 1, 30);
                _repo.AddDatabaseEvent(5, 20, 20, true);

                Assert.IsTrue(_repo.DoesCatalogExist(30));
                Assert.IsTrue(_repo.DoesUserExist(20));
                Assert.IsTrue(_repo.DoesStateExist(20));
                Assert.IsTrue(_repo.DoesEventExist(5));
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
