using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryLogicLayer;
using LibraryDataLayer;

namespace LibraryLogicLayerTests
{
    [TestClass]
    public class RepositoryTests
    {
        private Catalog CreateCatalog(int id = 1, string title = "Test Book", string author = "Author", int pages = 100) =>
            new Catalog { catalogId = id, title = title, author = author, nrOfPages = pages };

        private User CreateUser(int id = 1, string first = "John", string last = "Doe") =>
            new User { userId = id, firstName = first, lastName = last };

        private State CreateState(int id = 1, int books = 5) =>
            new State { stateId = id, nrOfBooks = books, catalog = CreateCatalog() };

        // -------------------- Catalog --------------------
        [TestMethod]
        public void CatalogRepository_Should_Add_And_Get_Catalog()
        {
            var repo = new CatalogRepository();
            repo.AddCatalog(1, "Title", "Author", 200);

            var result = repo.GetCatalogById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Title", result.title);
        }

        [TestMethod]
        public void CatalogRepository_Should_Remove_Catalog()
        {
            var repo = new CatalogRepository();
            repo.AddCatalog(1, "Title", "Author", 200);
            repo.RemoveCatalogById(1);

            var result = repo.GetCatalogById(1);
            Assert.IsNull(result);
        }

        // -------------------- User --------------------
        [TestMethod]
        public void UserRepository_Should_Add_And_Get_User()
        {
            var repo = new UserRepository();
            repo.AddUser(1, "Jane", "Smith");

            var result = repo.GetUserById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("Jane", result.firstName);
        }

        [TestMethod]
        public void UserRepository_Should_Remove_User()
        {
            var repo = new UserRepository();
            repo.AddUser(1, "Jane", "Smith");
            repo.RemoveUserById(1);

            var result = repo.GetUserById(1);
            Assert.IsNull(result);
        }

        // -------------------- State --------------------
        [TestMethod]
        public void StateRepository_Should_Add_And_Get_State()
        {
            var repo = new StateRepository();
            var catalog = CreateCatalog();

            repo.AddState(1, 10, catalog);
            var state = repo.GetStateById(1);

            Assert.IsNotNull(state);
            Assert.AreEqual(10, state.nrOfBooks);
        }

        [TestMethod]
        public void StateRepository_Should_Remove_State()
        {
            var repo = new StateRepository();
            repo.AddState(1, 10, CreateCatalog());
            repo.RemoveStateByID(1);

            var state = repo.GetStateById(1);
            Assert.IsNull(state);
        }

        // -------------------- Event --------------------
        [TestMethod]
        public void EventRepository_Should_Add_And_Get_DatabaseEvent()
        {
            var repo = new EventRepository();
            var employee = CreateUser();
            var state = CreateState();

            repo.AddDatabaseEvent(1, employee, state, true);

            var e = repo.GetEventById(1) as DatabaseEvent;
            Assert.IsNotNull(e);
            Assert.IsTrue(e.addition);
        }

        [TestMethod]
        public void EventRepository_Should_Add_And_Get_UserEvent()
        {
            var repo = new EventRepository();
            var employee = CreateUser();
            var state = CreateState();
            var user = CreateUser(2, "Alice", "Bob");

            repo.AddUserEvent(2, employee, state, user, false);

            var e = repo.GetEventById(2) as UserEvent;
            Assert.IsNotNull(e);
            Assert.AreEqual("Alice", e.user.firstName);
            Assert.IsFalse(e.borrowing);
        }

        [TestMethod]
        public void EventRepository_Should_Remove_Event()
        {
            var repo = new EventRepository();
            repo.AddDatabaseEvent(1, CreateUser(), CreateState(), true);
            repo.RemoveEventById(1);

            var e = repo.GetEventById(1);
            Assert.IsNull(e);
        }
    }

    [TestClass]
    public class ServiceTests
    {
        private LibraryDataService _service;
        private ICatalogRepository _catalogRepo;
        private IUserRepository _userRepo;
        private IEventRepository _eventRepo;
        private IStateRepository _stateRepo;

        [TestInitialize]
        public void Setup()
        {
            _catalogRepo = new CatalogRepository();
            _userRepo = new UserRepository();
            _eventRepo = new EventRepository();
            _stateRepo = new StateRepository();

            _service = new LibraryDataService(_catalogRepo, _userRepo, _eventRepo, _stateRepo);
        }

        [TestMethod]
        public void LibraryService_Should_Add_And_Get_Catalog()
        {
            _service.AddCatalog(1, "Book", "Writer", 123);
            var catalog = _service.GetCatalogById(1);

            Assert.IsNotNull(catalog);
            Assert.AreEqual("Book", catalog.title);
        }

        [TestMethod]
        public void LibraryService_Should_Add_And_Get_User()
        {
            _service.AddUser(1, "Emma", "Stone");
            var user = _service.GetUserById(1);

            Assert.IsNotNull(user);
            Assert.AreEqual("Emma", user.firstName);
        }

        [TestMethod]
        public void LibraryService_Should_Add_And_Get_State()
        {
            var catalog = new Catalog { catalogId = 1, title = "C#", author = "MS", nrOfPages = 300 };
            _service.AddState(1, 5, catalog);

            var state = _service.GetStateById(1);
            Assert.IsNotNull(state);
            Assert.AreEqual(5, state.nrOfBooks);
        }

        [TestMethod]
        public void LibraryService_Should_Add_And_Get_DatabaseEvent()
        {
            var emp = new User { userId = 1, firstName = "Staff", lastName = "One" };
            var state = new State { stateId = 1, nrOfBooks = 10, catalog = new Catalog() };

            _service.AddDatabaseEvent(1, emp, state, true);
            var ev = _service.GetEventById(1) as DatabaseEvent;

            Assert.IsNotNull(ev);
            Assert.IsTrue(ev.addition);
        }

        [TestMethod]
        public void LibraryService_Should_Add_And_Get_UserEvent()
        {
            var emp = new User { userId = 1, firstName = "Librarian", lastName = "Smith" };
            var user = new User { userId = 2, firstName = "Borrower", lastName = "Jones" };
            var state = new State { stateId = 1, nrOfBooks = 8, catalog = new Catalog() };

            _service.AddUserEvent(1, emp, state, user, true);
            var ev = _service.GetEventById(1) as UserEvent;

            Assert.IsNotNull(ev);
            Assert.AreEqual("Borrower", ev.user.firstName);
        }
    }
}
