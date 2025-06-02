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
                string testConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\Users\\damia\\Desktop\\IT SEM 4\\ProgTech\\LibraryDataCenter\\LibraryDataCenter\\Database1.mdf;Integrated Security=True;";
                repo = ILibraryDataRepository.CreateNewRepository(testConnectionString);

                // Wyczyœæ dane testowe
                foreach (var c in repo.GetAllCatalogs().ToList())
                    repo.RemoveCatalogById(c.CatalogId);

                foreach (var e in repo.GetAllEvents()) repo.RemoveEventById(e.EventId);
                foreach (var s in repo.GetAllStates()) repo.RemoveStateByID(s.StateId);
                foreach (var c in repo.GetAllCatalogs()) repo.RemoveCatalogById(c.CatalogId);
                foreach (var u in repo.GetAllUsers()) repo.RemoveUserById(u.UserId);
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

            [TestMethod]
            public void AddUser_And_GetUserById_Works()
            {
                repo.AddUser(1, "John", "Doe");
                var user = repo.GetUserById(1);

                Assert.IsNotNull(user);
                Assert.AreEqual("John", user.FirstName);
                Assert.AreEqual("Doe", user.LastName);
            }

            [TestMethod]
            public void RemoveUserById_RemovesUser()
            {
                repo.AddUser(2, "Jane", "Smith");
                repo.RemoveUserById(2);
                var user = repo.GetUserById(2);

                Assert.IsNull(user);
            }

            [TestMethod]
            public void AddState_And_GetStateById_Works()
            {
                repo.AddCatalog(10, "TestTitle", "TestAuthor", 150); 

                repo.AddState(5, 20, 10);
                var state = repo.GetStateById(5);

                Assert.IsNotNull(state);
                Assert.AreEqual(5, state.StateId);
                Assert.AreEqual(20, state.NrOfBooks);
                Assert.IsNotNull(state.Catalog);
                Assert.AreEqual(10, state.Catalog.CatalogId);
            }

            [TestMethod]
            public void RemoveStateById_RemovesState()
            {
                repo.AddCatalog(20, "AnotherTitle", "AnotherAuthor", 300);
                repo.AddState(7, 15, 20);

                repo.RemoveStateByID(7);
                var state = repo.GetStateById(7);

                Assert.IsNull(state);
            }

            [TestMethod]
            public void AddDatabaseEvent_And_GetEventById_Works()
            {
                repo.AddUser(1, "EmployeeFirst", "EmployeeLast");
                repo.AddCatalog(100, "CatalogForEvent", "Author", 100);
                repo.AddState(1, 10, 100);

                repo.AddDatabaseEvent(50, 1, 1, true);
                var evt = repo.GetEventById(50);

                Assert.IsNotNull(evt);
                Assert.AreEqual(50, evt.EventId);
                Assert.IsTrue(evt.Addition);
                Assert.IsNotNull(evt.Employee);
                Assert.AreEqual(1, evt.Employee.UserId);
                Assert.IsNotNull(evt.State);
                Assert.AreEqual(1, evt.State.StateId);
            }

            [TestMethod]
            public void AddUserEvent_And_GetEventById_Works()
            {
                repo.AddUser(1, "EmployeeFirst", "EmployeeLast");
                repo.AddUser(2, "UserFirst", "UserLast");
                repo.AddCatalog(200, "CatalogEvent", "Author", 200);
                repo.AddState(2, 30, 200);

                repo.AddUserEvent(60, 1, 2, 2, true);
                var evt = repo.GetEventById(60);

                Assert.IsNotNull(evt);
                Assert.AreEqual(60, evt.EventId);
                Assert.IsTrue(evt.Borrowing);
                Assert.IsNotNull(evt.Employee);
                Assert.AreEqual(1, evt.Employee.UserId);
                Assert.IsNotNull(evt.User);
                Assert.AreEqual(2, evt.User.UserId);
                Assert.IsNotNull(evt.State);
                Assert.AreEqual(2, evt.State.StateId);
            }

            [TestMethod]
            public void RemoveEventById_RemovesEvent()
            {
                repo.AddUser(1, "EmployeeFirst", "EmployeeLast");
                repo.AddCatalog(300, "CatalogRemoveEvent", "Author", 300);
                repo.AddState(3, 25, 300);

                repo.AddDatabaseEvent(70, 1, 3, false);
                repo.RemoveEventById(70);

                var evt = repo.GetEventById(70);
                Assert.IsNull(evt);
            }

            [TestMethod]
            public void DoesExistMethods_WorkCorrectly()
            {
                repo.AddCatalog(400, "ExistTitle", "ExistAuthor", 400);
                repo.AddUser(10, "ExistFirst", "ExistLast");
                repo.AddState(40, 12, 400);
                repo.AddDatabaseEvent(80, 10, 40, true);

                Assert.IsTrue(repo.DoesCatalogExist(400));
                Assert.IsTrue(repo.DoesUserExist(10));
                Assert.IsTrue(repo.DoesStateExist(40));
                Assert.IsTrue(repo.DoesEventExist(80));

                Assert.IsFalse(repo.DoesCatalogExist(-1));
                Assert.IsFalse(repo.DoesUserExist(-1));
                Assert.IsFalse(repo.DoesStateExist(-1));
                Assert.IsFalse(repo.DoesEventExist(-1));
            }

            

            [TestMethod]
            public void RemoveCatalogById_Works()
            {
                repo.AddCatalog(2, "Brave New World", "Huxley", 250);
                repo.RemoveCatalogById(2);

                var catalog = repo.GetCatalogById(2);
                Assert.IsNull(catalog);
            }

            [TestMethod]
            public void GetAllCatalogs_ReturnsCorrectCount()
            {
                repo.AddCatalog(3, "Book1", "Author1", 100);
                repo.AddCatalog(4, "Book2", "Author2", 200);

                var all = repo.GetAllCatalogs().ToList();
                Assert.AreEqual(2, all.Count);
            }

            [TestMethod]
            public void DoesCatalogExist_ReturnsCorrectValues()
            {
                repo.AddCatalog(5, "Exists", "Yes", 100);
                Assert.IsTrue(repo.DoesCatalogExist(5));
                Assert.IsFalse(repo.DoesCatalogExist(999));
            }

            // === User Tests ===

            

            [TestMethod]
            public void RemoveUserById_Works()
            {
                repo.AddUser(11, "Bob", "Jones");
                repo.RemoveUserById(11);
                Assert.IsNull(repo.GetUserById(11));
            }

            [TestMethod]
            public void GetAllUsers_ReturnsCorrectCount()
            {
                repo.AddUser(12, "Test", "User1");
                repo.AddUser(13, "Test", "User2");

                var users = repo.GetAllUsers().ToList();
                Assert.AreEqual(2, users.Count);
            }

            [TestMethod]
            public void DoesUserExist_Works()
            {
                repo.AddUser(14, "Charlie", "Exist");
                Assert.IsTrue(repo.DoesUserExist(14));
                Assert.IsFalse(repo.DoesUserExist(999));
            }

            // === State Tests ===

           

            [TestMethod]
            public void RemoveStateById_Works()
            {
                repo.AddCatalog(21, "TempBook", "Auth", 111);
                repo.AddState(101, 5, 21);

                repo.RemoveStateByID(101);
                Assert.IsNull(repo.GetStateById(101));
            }

            [TestMethod]
            public void GetAllStates_ReturnsCorrectCount()
            {
                repo.AddCatalog(22, "Book1", "A", 111);
                repo.AddCatalog(23, "Book2", "B", 222);
                repo.AddState(102, 4, 22);
                repo.AddState(103, 6, 23);

                var states = repo.GetAllStates().ToList();
                Assert.AreEqual(2, states.Count);
            }

            [TestMethod]
            public void DoesStateExist_Works()
            {
                repo.AddCatalog(24, "Cat", "Auth", 333);
                repo.AddState(104, 8, 24);

                Assert.IsTrue(repo.DoesStateExist(104));
                Assert.IsFalse(repo.DoesStateExist(999));
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
