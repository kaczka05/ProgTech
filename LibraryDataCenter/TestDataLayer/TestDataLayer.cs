using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using LibraryDataLayer;

namespace TestDataLayer
{
    // In-test implementation of interfaces
    public class TestBook : ICatalog
    {
        public int CatalogId { get; init; }
        public string Title { get; init; }
        public string Author { get; init; }
        public int NrOfPages { get; init; }

        public TestBook(int catalogId, string title, string author, int nrOfPages)
        {
            CatalogId = catalogId;
            Title = title;
            Author = author;
            NrOfPages = nrOfPages;
        }
    }

    public class TestUser : IUser
    {
        public int UserId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }

        public TestUser(int id, string first, string last)
        {
            UserId = id;
            FirstName = first;
            LastName = last;
        }
    }

    public class TestState : IState
    {
        public int StateId { get; init; }
        public int NrOfBooks { get; init; }
        public ICatalog Catalog { get; init; }

        public TestState(int id, int nr, ICatalog catalog)
        {
            StateId = id;
            NrOfBooks = nr;
            Catalog = catalog;
        }
    }

    // Now implement IEvent so we can store in List<IEvent>
    public class TestDatabaseEvent : Event, IDatabaseEvent
    {
        public int EventId { get; init; }
        public IUser Employee { get; init; }
        public IState State { get; init; }
        public bool Addition { get; init; }


        public TestDatabaseEvent(int id, IUser emp, IState st, bool add)
        {
            EventId = id;
            Employee = emp;
            State = st;
            Addition = add;
        }
    }

    public class TestUserEvent : IEvent, IUserEvent
    {
        public int EventId { get; init; }
        public IUser Employee { get; init; }
        public IState State { get; init; }
        public IUser User { get; init; }
        public bool Borrowing { get; init; }

        public TestUserEvent(int id, IUser emp, IState st, IUser usr, bool bor)
        {
            EventId = id;
            Employee = emp;
            State = st;
            User = usr;
            Borrowing = bor;
        }
    }

    public class TestLibraryDataContext : ILibraryDataContext
    {
        public List<IUser> Users { get; init; } = new List<IUser>();
        public List<ICatalog> Catalogs { get; init; } = new List<ICatalog>();
        public List<IEvent> Events { get; init; } = new List<IEvent>();
        public List<IState> States { get; init; } = new List<IState>();
    }

    public class TestLibraryDataRepository : ILibraryDataRepository
    {
        private readonly ILibraryDataContext _ctx;
        public TestLibraryDataRepository(ILibraryDataContext ctx) { _ctx = ctx; }
        public void AddCatalog(int catalogId, string title, string author, int nrOfPages)
            => _ctx.Catalogs.Add(new TestBook(catalogId, title, author, nrOfPages));

        public void RemoveCatalogById(int id)
        {
            var c = _ctx.Catalogs.FirstOrDefault(x => x.catalogId == id);
            if (c != null) _ctx.Catalogs.Remove(c);
        }

        public void AddUser(int userId, string firstName, string lastName)
            => _ctx.Users.Add(new TestUser(userId, firstName, lastName));

        public void RemoveUserById(int id)
        {
            var u = _ctx.Users.FirstOrDefault(x => x.UserId == id);
            if (u != null) _ctx.Users.Remove(u);
        }

        public void AddDatabaseEvent(int eventId, int employeeId, int stateId, bool addition)
        {
            var emp = _ctx.Users.First(x => x.UserId == employeeId);
            var st = _ctx.States.First(x => x.StateId == stateId);
            _ctx.Events.Add(new TestDatabaseEvent(eventId, emp, st, addition));
        }

        public void AddUserEvent(int eventId, int employeeId, int stateId, int userId, bool borrowing)
        {
            var emp = _ctx.Users.First(x => x.UserId == employeeId);
            var st = _ctx.States.First(x => x.StateId == stateId);
            var usr = _ctx.Users.First(x => x.UserId == userId);
            _ctx.Events.Add(new TestUserEvent(eventId, emp, st, usr, borrowing));
        }

        public void RemoveEventById(int id)
        {
            var e = _ctx.Events.FirstOrDefault(x => x.EventId == id);
            if (e != null) _ctx.Events.Remove(e);
        }

        public void AddState(int stateId, int nrOfBooks, int catalogId)
        {
            var cat = _ctx.Catalogs.First(x => x.catalogId == catalogId);
            _ctx.States.Add(new TestState(stateId, nrOfBooks, cat));
        }

        public void RemoveStateByID(int id)
        {
            var s = _ctx.States.FirstOrDefault(x => x.StateId == id);
            if (s != null) _ctx.States.Remove(s);
        }

        public bool DoesCatalogExist(int id) => _ctx.Catalogs.Any(x => x.catalogId == id);
        public bool DoesUserExist(int id) => _ctx.Users.Any(x => x.UserId == id);
        public bool DoesEventExist(int id) => _ctx.Events.Any(x => x.EventId == id);
        public bool DoesStateExist(int id) => _ctx.States.Any(x => x.StateId == id);
    }

    // Tests
    [TestClass]
    public class FullDataLayerTests
    {
        private TestLibraryDataContext ctx;
        private TestLibraryDataRepository repo;

        [TestInitialize]
        public void Init()
        {
            ctx = new TestLibraryDataContext();
            repo = new TestLibraryDataRepository(ctx);
        }

        [TestMethod]
        public void Catalog_AddRemove_CheckExists()
        {
            repo.AddCatalog(1, "T", "A", 10);
            Assert.IsTrue(repo.DoesCatalogExist(1));
            repo.RemoveCatalogById(1);
            Assert.IsFalse(repo.DoesCatalogExist(1));
        }

        [TestMethod]
        public void User_AddRemove_CheckExists()
        {
            repo.AddUser(2, "F", "L");
            Assert.IsTrue(repo.DoesUserExist(2));
            repo.RemoveUserById(2);
            Assert.IsFalse(repo.DoesUserExist(2));
        }

        [TestMethod]
        public void State_AddRemove_CheckExists()
        {
            repo.AddCatalog(3, "T3", "A3", 30);
            repo.AddState(4, 5, 3);
            Assert.IsTrue(repo.DoesStateExist(4));
            repo.RemoveStateByID(4);
            Assert.IsFalse(repo.DoesStateExist(4));
        }

        [TestMethod]
        public void Events_AddRemove_CheckExists()
        {
            // Setup
            repo.AddUser(5, "E", "L");
            repo.AddCatalog(6, "T6", "A6", 60);
            repo.AddState(7, 7, 6);

            // DatabaseEvent
            repo.AddDatabaseEvent(10, 5, 7, true);
            Assert.IsTrue(repo.DoesEventExist(10));
            repo.RemoveEventById(10);
            Assert.IsFalse(repo.DoesEventExist(10));

            // UserEvent
            repo.AddUser(8, "U8", "L8");
            repo.AddUserEvent(11, 5, 7, 8, false);
            Assert.IsTrue(repo.DoesEventExist(11));
        }
    }
}