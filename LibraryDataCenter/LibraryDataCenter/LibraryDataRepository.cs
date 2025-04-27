using LibraryDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryLogicLayer
{
    internal class LogicCatalog : ICatalog
       {
            public int catalogId { get; init; }
    public string title { get; init; }
    public string author { get; init; }
    public int nrOfPages { get; init; }
    }
    internal class CatalogRepository : ICatalogRepository
    {
        public List<ICatalog> Catalogs { get; init; } = new List<ICatalog>();

        public void AddCatalog(int catalogId, string title, string author, int numberOfPages)
        {
            var catalog = new LogicCatalog
            {
                catalogId = catalogId,
                title = title,
                author = author,
                nrOfPages = numberOfPages
            };
            Catalogs.Add(catalog);
        }

        public void RemoveCatalogById(int id)
        {
            var catalog = Catalogs.FirstOrDefault(c => c.catalogId == id);
            if (catalog != null)
            {
                Catalogs.Remove(catalog);
            }
        }

        public ICatalog GetCatalogById(int id)
        {
            return Catalogs.FirstOrDefault(c => c.catalogId == id);
        }
    }

    internal class LogicUser : IUser
    {
        public int UserId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }

    internal class UserRepository : IUserRepository
    {
        public List<IUser> Users { get; init; } = new List<IUser>();

        public void AddUser(int userId, string firstName, string lastName)
        {
            var user = new LogicUser
            {
                UserId = userId,
                FirstName = firstName,
                LastName = lastName
            };
            Users.Add(user);
        }

        public void RemoveUserById(int id)
        {
            var user = Users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                Users.Remove(user);
            }
        }

        public IUser GetUserById(int id)
        {
            return Users.FirstOrDefault(u => u.UserId == id);
        }
    }

    internal class LogicEvent : IEvent
    {
        public int EventId { get; init; }
        public IUser Employee { get; init; }
        public IState State { get; init; }
    }

    internal class EventRepository : IEventRepository
    {
        public List<IEvent> Events { get; init; } = new List<IEvent>();

        public void AddDatabaseEvent(int eventId, IUser employee, IState state, bool addition)
        {
            var databaseEvent = new LogicDatabaseEvent
            {
                EventId = eventId,
                Employee = employee,
                State = state,
                Addition = addition
            };
            Events.Add(databaseEvent); // Poprawiono dodanie instancji
        }

        public void AddUserEvent(int eventId, IUser employee, IState state, IUser user, bool borrowing)
        {
            var userEvent = new LogicUserEvent
            {
                EventId = eventId,
                Employee = employee,
                State = state,
                User = user,
                Borrowing = borrowing
            };
            Events.Add(userEvent); // Poprawiono dodanie instancji
        }

        public void RemoveEventById(int id)
        {
            var eventObj = Events.FirstOrDefault(e => e.EventId == id);
            if (eventObj != null)
            {
                Events.Remove(eventObj);
            }
        }

        public IEvent GetEventById(int id)
        {
            return Events.FirstOrDefault(e => e.EventId == id);
        }
    }

    internal class LogicState : IState
    {
        public int StateId { get; init; }
        public int NrOfBooks { get; init; }
        public ICatalog Catalog { get; init; }
    }

    internal class StateRepository : IStateRepository
    {
        public List<IState> States { get; init; } = new List<IState>();

        public void AddState(int stateId, int nrOfBooks, ICatalog catalog)
        {
            var state = new LogicState
            {
                StateId = stateId,
                NrOfBooks = nrOfBooks,
                Catalog = catalog
            };
            States.Add(state);
        }

        public void RemoveStateByID(int id)
        {
            var state = States.FirstOrDefault(s => s.StateId == id);
            if (state != null)
            {
                States.Remove(state);
            }
        }

        public IState GetStateById(int id)
        {
            return States.FirstOrDefault(s => s.StateId == id);
        }
    }

    internal class LogicDatabaseEvent : LogicEvent, IDatabaseEvent
    {
        public bool Addition { get; init; }
    }

    internal class LogicUserEvent : LogicEvent, IUserEvent
    {
        public IUser User { get; init; }
        public bool Borrowing { get; init; }
    }

}
