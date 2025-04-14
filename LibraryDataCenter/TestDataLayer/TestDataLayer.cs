using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryDataLayer;
using System.Collections.Generic;

namespace LibraryDataLayerTests
{
    [TestClass]
    public class LibraryTests
    {
        private User CreateTestUser(int id = 1, string firstName = "John", string lastName = "Doe")
        {
            return new User
            {
                userId = id,
                firstName = firstName,
                lastName = lastName
            };
        }

        private LibraryCatalog CreateTestCatalog(int id = 1, string title = "Book", string author = "Author", int pages = 100)
        {
            return new LibraryCatalog
            {
                catalogId = id,
                title = title,
                author = author,
                nrOfPages = pages
            };
        }

        [TestMethod]
        public void Catalog_Should_Store_Data_Correctly()
        {
            var catalog = new LibraryCatalog
            {
                catalogId = 1,
                title = "1984",
                author = "George Orwell",
                nrOfPages = 328
            };

            Assert.AreEqual(1, catalog.catalogId);
            Assert.AreEqual("1984", catalog.title);
            Assert.AreEqual("George Orwell", catalog.author);
            Assert.AreEqual(328, catalog.nrOfPages);
        }

        [TestMethod]
        public void User_Should_Store_Data_Correctly()
        {
            var user = CreateTestUser(2, "Alice", "Smith");

            Assert.AreEqual(2, user.userId);
            Assert.AreEqual("Alice", user.firstName);
            Assert.AreEqual("Smith", user.lastName);
        }

        [TestMethod]
        public void State_Should_Link_To_Catalog_And_Store_Data()
        {
            var catalog = CreateTestCatalog();
            var state = new LibraryState
            {
                stateId = 10,
                nrOfBooks = 5,
                catalog = catalog
            };

            Assert.AreEqual(10, state.stateId);
            Assert.AreEqual(5, state.nrOfBooks);
            Assert.AreSame(catalog, state.catalog);
        }

        [TestMethod]
        public void Event_Should_Store_Employee_And_State()
        {
            var user = CreateTestUser();
            var state = new LibraryState { stateId = 1, nrOfBooks = 3 };
            var ev = new LibraryEvent
            {
                eventId = 7,
                employee = user,
                state = state
            };

            Assert.AreEqual(7, ev.eventId);
            Assert.AreSame(user, ev.employee);
            Assert.AreSame(state, ev.state);
        }

        [TestMethod]
        public void DatabaseEvent_Should_Extend_Event_And_Add_Addition_Flag()
        {
            var dbEvent = new LibraryDatabaseEvent
            {
                eventId = 99,
                addition = true
            };

            Assert.AreEqual(99, dbEvent.eventId);
            Assert.IsTrue(dbEvent.addition);
        }

        [TestMethod]
        public void UserEvent_Should_Extend_Event_And_Link_User()
        {
            var user = CreateTestUser();
            var userEvent = new LibraryUserEvent
            {
                eventId = 23,
                user = user,
                borrowing = false
            };

            Assert.AreEqual(23, userEvent.eventId);
            Assert.AreSame(user, userEvent.user);
            Assert.IsFalse(userEvent.borrowing);
        }

        [TestMethod]
        public void DataContext_Should_Initialize_All_Collections()
        {
            var context = new LibraryDataContext();

            Assert.IsNotNull(context.Users);
            Assert.IsNotNull(context.Catalogs);
            Assert.IsNotNull(context.Events);
            Assert.IsNotNull(context.States);

            Assert.IsInstanceOfType(context.Users, typeof(List<User>));
            Assert.IsInstanceOfType(context.Catalogs, typeof(List<LibraryCatalog>));
            Assert.IsInstanceOfType(context.Events, typeof(List<LibraryEvent>));
            Assert.IsInstanceOfType(context.States, typeof(List<LibraryState>));
        }

        
        private LibraryState CreateTestState(int id = 1, int books = 10)
        {
            return new LibraryState
            {
                stateId = id,
                nrOfBooks = books,
                catalog = CreateTestCatalog()
            };
        }

        [TestMethod]
        public void Data_Generators_Should_Produce_Valid_Objects()
        {
            var user = CreateTestUser();
            var catalog = CreateTestCatalog();
            var state = CreateTestState();

            Assert.IsNotNull(user);
            Assert.IsNotNull(catalog);
            Assert.IsNotNull(state);
            Assert.IsNotNull(state.catalog);
        }
    }
}
